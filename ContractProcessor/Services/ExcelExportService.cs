using ClosedXML.Excel;

namespace ContractProcessor.Services;

public class ExcelExportService : IExportService
{
    public void Export(string filePath, List<Dictionary<string, string>> rows, List<string> columns)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Contracts");

        for (int i = 0; i < columns.Count; i++)
        {
            var cell = worksheet.Cell(1, i + 1);
            cell.Value = columns[i];
            cell.Style.Font.Bold = true;
        }

        for (int r = 0; r < rows.Count; r++)
        {
            for (int c = 0; c < columns.Count; c++)
            {
                var value = rows[r].TryGetValue(columns[c], out var v) ? v : string.Empty;
                worksheet.Cell(r + 2, c + 1).Value = value;
            }
        }

        worksheet.Columns().AdjustToContents();
        workbook.SaveAs(filePath);
    }
}
