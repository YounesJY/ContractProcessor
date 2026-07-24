using ContractProcessor.Constants;
using ContractProcessor.Data;
using ContractProcessor.Helpers;
using ContractProcessor.Models;
using ContractProcessor.Services;

namespace ContractProcessor.Forms;

public partial class MainForm : Form
{
    private readonly DatabaseHelper _db;
    private readonly IPdfProcessor _pdfProcessor;
    private readonly ExtractionService _extractionService;
    private readonly AppSettings _settings;
    private List<Contract> _contracts = new();

    public MainForm(string dbPath, AppSettings settings)
    {
        InitializeComponent();
        _db = new DatabaseHelper(dbPath);
        _pdfProcessor = new PdfProcessor();
        _extractionService = new ExtractionService(settings.UseAI, settings.AIModel);
        _settings = settings;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        LoadCategories();
        LoadContracts();
    }

    private void LoadCategories()
    {
        var categories = _db.GetCategories();
        cmbFilter.Items.Clear();
        cmbFilter.Items.Add("All");
        foreach (var cat in categories)
            cmbFilter.Items.Add(cat);
        cmbFilter.SelectedIndex = 0;
    }

    private void LoadContracts(string? categoryFilter = null)
    {
        _contracts = _db.GetAllContracts(categoryFilter);
        dgvContracts.DataSource = null;
        dgvContracts.DataSource = _contracts.Select(c => new
        {
            c.Id,
            c.FileName,
            c.Category,
            Status = c.ProcessingStatus,
            Date = c.UploadDate.ToString("yyyy-MM-dd HH:mm")
        }).ToList();
    }

    private async void btnUpload_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = "PDF files (*.pdf)|*.pdf";
        dialog.Multiselect = true;
        dialog.Title = "Select PDF contracts";

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        int imported = 0;
        int skipped = 0;
        int aiCount = 0;

        foreach (var filePath in dialog.FileNames)
        {
            var hash = FileHashHelper.ComputeSha256(filePath);

            if (_db.DuplicateExists(hash))
            {
                skipped++;
                continue;
            }

            var fileName = Path.GetFileName(filePath);
            var text = await Task.Run(() => _pdfProcessor.ExtractText(filePath));
            var category = CategoryDetector.Detect(text);
            var extraction = await _extractionService.ExtractAsync(text, category);

            var contractsDir = Path.Combine(_settings.RootFolder, AppConstants.ContractsFolder, category);
            Directory.CreateDirectory(contractsDir);
            var destPath = Path.Combine(contractsDir, fileName);
            File.Copy(filePath, destPath, true);

            var relativePath = Path.Combine(AppConstants.ContractsFolder, category, fileName);

            var contract = new Contract
            {
                FileName = fileName,
                FilePath = relativePath,
                Category = category,
                FileHash = hash,
                UploadDate = DateTime.Now,
                ExtractedData = System.Text.Json.JsonSerializer.Serialize(extraction.Fields),
                SelectedFields = "[]",
                ProcessingStatus = AppConstants.ProcessingStatus.Processed
            };

            _db.InsertContract(contract);
            imported++;

            if (extraction.Method == "AI")
                aiCount++;
        }

        LoadContracts(cmbFilter.SelectedItem?.ToString());

        if (skipped > 0)
            NotificationHelper.ShowWarning($"{skipped} duplicate(s) skipped.");
        if (imported > 0)
        {
            var method = aiCount > 0 ? "AI" : "Regex";
            NotificationHelper.ShowSuccess($"{imported} contract(s) imported via {method}.");
        }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        if (_contracts.Count == 0)
        {
            NotificationHelper.ShowWarning("No contracts to export.");
            return;
        }

        var allFields = new HashSet<string>();
        foreach (var c in _contracts)
        {
            var extracted = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(c.ExtractedData);
            var selected = System.Text.Json.JsonSerializer.Deserialize<List<string>>(c.SelectedFields);

            if (selected != null && selected.Count > 0)
            {
                foreach (var f in selected) allFields.Add(f);
            }
            else if (extracted != null)
            {
                foreach (var f in extracted.Keys) allFields.Add(f);
            }
        }

        if (allFields.Count == 0)
        {
            NotificationHelper.ShowWarning("No fields selected for export. Select fields first.");
            return;
        }

        var columns = allFields.OrderBy(f => f).ToList();
        var rows = new List<Dictionary<string, string>>();

        foreach (var c in _contracts)
        {
            var extracted = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(c.ExtractedData) ?? new();
            var selected = System.Text.Json.JsonSerializer.Deserialize<List<string>>(c.SelectedFields) ?? new();

            var row = new Dictionary<string, string>();
            var fieldsToExport = selected.Count > 0 ? selected : columns;

            foreach (var col in columns)
            {
                row[col] = fieldsToExport.Contains(col) && extracted.TryGetValue(col, out var val) ? val : string.Empty;
            }
            rows.Add(row);
        }

        using var dialog = new SaveFileDialog();
        dialog.Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv";
        dialog.FileName = $"export_{DateTime.Now:yyyyMMdd_HHmmss}";

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var ext = Path.GetExtension(dialog.FileName).ToLowerInvariant();
        var format = ext == ".csv" ? AppConstants.ExportFormats.Csv : AppConstants.ExportFormats.Excel;

        try
        {
            var service = ExportServiceFactory.Create(format);
            service.Export(dialog.FileName, rows, columns);
            NotificationHelper.ShowSuccess($"Exported to {dialog.FileName}");
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowError($"Export failed: {ex.Message}");
        }
    }

    private void btnSelectFields_Click(object sender, EventArgs e)
    {
        if (dgvContracts.CurrentRow == null) return;

        var id = Convert.ToInt32(dgvContracts.CurrentRow.Cells["Id"].Value);
        var contract = _db.GetContract(id);
        if (contract == null) return;

        var extracted = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(contract.ExtractedData) ?? new();
        var selected = System.Text.Json.JsonSerializer.Deserialize<List<string>>(contract.SelectedFields) ?? new();

        using var form = new FieldSelectionForm(extracted.Keys.ToList(), selected);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updatedFields = System.Text.Json.JsonSerializer.Serialize(form.SelectedFields);
            _db.UpdateSelectedFields(id, updatedFields);
            NotificationHelper.ShowSuccess("Fields updated.");
        }
    }

    private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadContracts(cmbFilter.SelectedItem?.ToString());
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dgvContracts.CurrentRow == null) return;

        if (!NotificationHelper.Confirm("Delete this contract?")) return;

        var id = Convert.ToInt32(dgvContracts.CurrentRow.Cells["Id"].Value);
        _db.DeleteContract(id);
        LoadContracts(cmbFilter.SelectedItem?.ToString());
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
        using var form = new SettingsForm(_settings, _db);
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadCategories();
        }
    }

    private void btnViewText_Click(object sender, EventArgs e)
    {
        if (dgvContracts.CurrentRow == null) return;

        var id = Convert.ToInt32(dgvContracts.CurrentRow.Cells["Id"].Value);
        var contract = _db.GetContract(id);
        if (contract == null) return;

        var extracted = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(contract.ExtractedData) ?? new();

        var form = new Form();
        form.Text = $"Extracted Data - {contract.FileName}";
        form.Size = new Size(600, 450);
        form.StartPosition = FormStartPosition.CenterParent;

        var txt = new TextBox();
        txt.Dock = DockStyle.Fill;
        txt.Multiline = true;
        txt.ReadOnly = true;
        txt.ScrollBars = ScrollBars.Both;
        txt.Font = new Font("Consolas", 10F);
        txt.Text = System.Text.Json.JsonSerializer.Serialize(extracted, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

        form.Controls.Add(txt);
        form.ShowDialog();
    }

    private void btnManualExtract_Click(object sender, EventArgs e)
    {
        if (dgvContracts.CurrentRow == null) return;

        var id = Convert.ToInt32(dgvContracts.CurrentRow.Cells["Id"].Value);
        var contract = _db.GetContract(id);
        if (contract == null) return;

        // Get the PDF text
        var pdfPath = Path.Combine(_settings.RootFolder, contract.FilePath);
        if (!File.Exists(pdfPath))
        {
            NotificationHelper.ShowError("PDF file not found on disk.");
            return;
        }

        var pdfText = _pdfProcessor.ExtractText(pdfPath);
        var currentFields = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(contract.ExtractedData) ?? new();

        using var form = new ManualExtractForm(pdfText, currentFields);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updatedData = System.Text.Json.JsonSerializer.Serialize(form.ExtractedFields);
            _db.UpdateExtractedData(id, updatedData, AppConstants.ProcessingStatus.Processed);
            NotificationHelper.ShowSuccess("Fields extracted manually.");
        }
    }

}
