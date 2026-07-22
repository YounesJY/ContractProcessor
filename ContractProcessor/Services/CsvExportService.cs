using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace ContractProcessor.Services;

public class CsvExportService : IExportService
{
    public void Export(string filePath, List<Dictionary<string, string>> rows, List<string> columns)
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));

        foreach (var col in columns)
        {
            csv.WriteField(col);
        }
        csv.NextRecord();

        foreach (var row in rows)
        {
            foreach (var col in columns)
            {
                csv.WriteField(row.TryGetValue(col, out var value) ? value : string.Empty);
            }
            csv.NextRecord();
        }
    }
}
