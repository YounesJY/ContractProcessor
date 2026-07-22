namespace ContractProcessor.Constants;

public static class AppConstants
{
    public const string DbFileName = "contract_history.db";
    public const string AppSettingsFile = "appsettings.json";
    public const string ContractsFolder = "Contracts";
    public const string ExportsFolder = "Exports";
    public const string AppDataFolder = "AppData";

    public static class Categories
    {
        public const string AT = "AT";
        public const string AUTO = "AUTO";
        public const string MRH = "MRH";
        public const string Unknown = "Unknown";
    }

    public static class ProcessingStatus
    {
        public const string Pending = "Pending";
        public const string Processed = "Processed";
        public const string Failed = "Failed";
    }

    public static class ExportFormats
    {
        public const string Csv = "CSV";
        public const string Excel = "Excel";
    }
}
