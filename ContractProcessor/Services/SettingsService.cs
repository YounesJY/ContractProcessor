using System.Text.Json;
using ContractProcessor.Constants;

namespace ContractProcessor.Services;

public static class SettingsService
{
    private static string GetSettingsPath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConstants.AppSettingsFile);
    }

    public static Models.AppSettings Load()
    {
        var path = GetSettingsPath();
        if (!File.Exists(path))
        {
            var defaultSettings = new Models.AppSettings
            {
                RootFolder = string.Empty,
                Categories = new List<string> { AppConstants.Categories.AT, AppConstants.Categories.AUTO, AppConstants.Categories.MRH }
            };
            Save(defaultSettings);
            return defaultSettings;
        }

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<Models.AppSettings>(json) ?? new Models.AppSettings();
    }

    public static void Save(Models.AppSettings settings)
    {
        var path = GetSettingsPath();
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }
}
