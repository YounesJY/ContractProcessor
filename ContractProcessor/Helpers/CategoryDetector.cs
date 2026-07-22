using ContractProcessor.Constants;

namespace ContractProcessor.Helpers;

public static class CategoryDetector
{
    private static readonly Dictionary<string, string[]> CategoryKeywords = new()
    {
        { AppConstants.Categories.AT, new[] { "assurance temporaire", "AT", "décès", "vie" } },
        { AppConstants.Categories.AUTO, new[] { "automobile", "AUTO", "véhicule", "auto" } },
        { AppConstants.Categories.MRH, new[] { "multirisque habitation", "MRH", "habitation", "logement" } }
    };

    public static string Detect(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return AppConstants.Categories.Unknown;

        var lowerText = text.ToLowerInvariant();

        foreach (var (category, keywords) in CategoryKeywords)
        {
            if (keywords.Any(kw => lowerText.Contains(kw.ToLowerInvariant())))
                return category;
        }

        return AppConstants.Categories.Unknown;
    }
}
