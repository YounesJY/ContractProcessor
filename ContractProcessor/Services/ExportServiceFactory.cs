using ContractProcessor.Constants;

namespace ContractProcessor.Services;

public static class ExportServiceFactory
{
    public static IExportService Create(string format)
    {
        return format.ToLowerInvariant() switch
        {
            "csv" => new CsvExportService(),
            "excel" or "xlsx" => new ExcelExportService(),
            _ => throw new ArgumentException($"Unsupported export format: {format}")
        };
    }
}
