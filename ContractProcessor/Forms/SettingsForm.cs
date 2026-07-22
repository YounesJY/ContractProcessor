using ContractProcessor.Data;
using ContractProcessor.Helpers;
using ContractProcessor.Models;
using ContractProcessor.Services;

namespace ContractProcessor.Forms;

public partial class SettingsForm : Form
{
    private readonly AppSettings _settings;
    private readonly DatabaseHelper _db;

    public SettingsForm(AppSettings settings, DatabaseHelper db)
    {
        InitializeComponent();
        _settings = settings;
        _db = db;
        LoadSettings();
        LoadCategories();
    }

    private void LoadSettings()
    {
        txtRootFolder.Text = _settings.RootFolder;
    }

    private void LoadCategories()
    {
        lstCategories.Items.Clear();
        foreach (var cat in _db.GetCategories())
            lstCategories.Items.Add(cat);
    }

    private void btnBrowseFolder_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog();
        dialog.SelectedPath = _settings.RootFolder;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtRootFolder.Text = dialog.SelectedPath;
        }
    }

    private void btnAddCategory_Click(object sender, EventArgs e)
    {
        var name = txtNewCategory.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            NotificationHelper.ShowWarning("Enter a category name.");
            return;
        }

        _db.AddCategory(name);
        txtNewCategory.Text = string.Empty;
        LoadCategories();
    }

    private void btnRemoveCategory_Click(object sender, EventArgs e)
    {
        if (lstCategories.SelectedItem == null) return;

        var name = lstCategories.SelectedItem.ToString()!;
        if (!NotificationHelper.Confirm($"Remove category '{name}'?"))
            return;

        _db.DeleteCategory(name);
        LoadCategories();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        _settings.RootFolder = txtRootFolder.Text;
        _settings.Categories = lstCategories.Items.Cast<string>().ToList();
        SettingsService.Save(_settings);
        NotificationHelper.ShowSuccess("Settings saved.");
        DialogResult = DialogResult.OK;
        Close();
    }
}
