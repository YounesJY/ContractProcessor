namespace ContractProcessor.Forms;

public partial class ManualExtractForm : Form
{
    private readonly string _pdfText;
    private readonly Dictionary<string, string> _currentFields;

    public Dictionary<string, string> ExtractedFields { get; private set; }

    // Common insurance contract fields
    private static readonly string[] FieldNames = new[]
    {
        "Police N°",
        "N° Client",
        "Type de contrat",
        "Souscripteur",
        "Assuré",
        "Adresse",
        "Téléphone",
        "Date d'effet",
        "Date d'échéance",
        "Code Intermédiaire",
        "Immatriculation",
        "Marque véhicule",
        "N° Châssis",
        "Capital décès",
        "Bénéficiaire",
        "Surface",
        "Valeur locative"
    };

    public ManualExtractForm(string pdfText, Dictionary<string, string>? existingFields = null)
    {
        InitializeComponent();
        _pdfText = pdfText;
        _currentFields = existingFields ?? new();
        ExtractedFields = new();
        PopulateFields();
    }

    private void PopulateFields()
    {
        // PDF text on the left
        txtPdfText.Text = _pdfText;

        // Create input fields on the right
        var yPos = 10;
        foreach (var fieldName in FieldNames)
        {
            var lbl = new Label();
            lbl.Text = fieldName + ":";
            lbl.Location = new Point(10, yPos + 3);
            lbl.Size = new Size(130, 20);
            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(50, 50, 60);

            var txt = new TextBox();
            txt.Name = "txt_" + fieldName;
            txt.Location = new Point(145, yPos);
            txt.Size = new Size(280, 25);
            txt.Font = new Font("Segoe UI", 9F);

            // Pre-fill if we have existing value
            if (_currentFields.TryGetValue(fieldName, out var val))
                txt.Text = val;

            panelFields.Controls.Add(lbl);
            panelFields.Controls.Add(txt);

            yPos += 32;
        }
    }

    private void btnConfirm_Click(object sender, EventArgs e)
    {
        ExtractedFields = new();

        foreach (Control ctrl in panelFields.Controls)
        {
            if (ctrl is TextBox txt && txt.Name.StartsWith("txt_"))
            {
                var fieldName = txt.Name[4..]; // Remove "txt_" prefix
                var value = txt.Text.Trim();
                if (!string.IsNullOrEmpty(value))
                    ExtractedFields[fieldName] = value;
            }
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
