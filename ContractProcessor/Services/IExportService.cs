namespace ContractProcessor.Services;

public interface IExportService
{
    void Export(string filePath, List<Dictionary<string, string>> rows, List<string> columns);
}
