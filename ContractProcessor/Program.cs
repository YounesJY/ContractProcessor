using ContractProcessor.Constants;

namespace ContractProcessor;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var settings = Services.SettingsService.Load();

        if (string.IsNullOrEmpty(settings.RootFolder) || !Directory.Exists(settings.RootFolder))
        {
            using var dialog = new FolderBrowserDialog();
            dialog.Description = "Select or create a root folder for ContractProcessor";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("A root folder is required to run the application.", "Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            settings.RootFolder = dialog.SelectedPath;
            Services.SettingsService.Save(settings);
        }

        var root = settings.RootFolder;
        Directory.CreateDirectory(Path.Combine(root, AppConstants.ContractsFolder));
        Directory.CreateDirectory(Path.Combine(root, AppConstants.ExportsFolder));

        var dbPath = Path.Combine(root, AppConstants.AppDataFolder, AppConstants.DbFileName);
        var initializer = new Data.DatabaseInitializer(dbPath);
        initializer.Initialize();

        Application.Run(new Forms.MainForm(dbPath, settings));
    }
}
