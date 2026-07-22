using UglyToad.PdfPig;

namespace ContractProcessor.Services;

public class PdfProcessor : IPdfProcessor
{
    public string ExtractText(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"PDF file not found: {filePath}");

        using var document = PdfDocument.Open(filePath);
        var text = string.Empty;

        foreach (var page in document.GetPages())
        {
            text += page.Text + Environment.NewLine;
        }

        return text;
    }
}
