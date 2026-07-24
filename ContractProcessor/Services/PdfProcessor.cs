using System.Text.RegularExpressions;
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
            var pageText = page.Text;
            text += pageText + Environment.NewLine;
        }

        return CleanText(text);
    }

    private string CleanText(string raw)
    {
        if (string.IsNullOrEmpty(raw))
            return raw;

        // Fix common PDF extraction issues
        var cleaned = raw;

        // Remove control characters but keep newlines
        cleaned = Regex.Replace(cleaned, @"[\x00-\x08\x0B\x0C\x0E-\x1F]", "");

        // Fix broken words (PDF sometimes splits words across lines)
        cleaned = Regex.Replace(cleaned, @"(\S)\n(\S)", "$1$2");

        // Normalize whitespace (multiple spaces → single space)
        cleaned = Regex.Replace(cleaned, @"[^\S\n]+", " ");

        // Remove empty lines
        cleaned = Regex.Replace(cleaned, @"\n{3,}", "\n\n");

        // Fix common OCR-like issues
        cleaned = cleaned.Replace("C O N D I T I O N S", "CONDITIONS");
        cleaned = cleaned.Replace("P A R T I C U L I E R E S", "PARTICULIERES");
        cleaned = cleaned.Replace("P A R T I C U L I È R E S", "PARTICULIERES");

        return cleaned.Trim();
    }
}
