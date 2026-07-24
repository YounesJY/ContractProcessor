namespace ContractProcessor.Services;

public class ExtractionResult
{
    public Dictionary<string, string> Fields { get; set; } = new();
    public string Method { get; set; } = "regex";
}

public class ExtractionService
{
    private readonly OllamaService? _ollama;
    private readonly bool _useAI;

    public ExtractionService(bool useAI, string model = "llama3.2")
    {
        _useAI = useAI;
        if (_useAI)
            _ollama = new OllamaService(model);
    }

    public async Task<ExtractionResult> ExtractAsync(string pdfText, string category)
    {
        DebugLogger.Log($"ExtractAsync called. useAI={_useAI}, ollama={_ollama != null}, textLen={pdfText.Length}");

        if (_useAI && _ollama != null)
        {
            try
            {
                DebugLogger.Log("Checking Ollama availability...");
                var available = await _ollama.IsAvailableAsync();
                DebugLogger.Log($"Ollama available: {available}");

                if (available)
                {
                    var fieldNames = GetFieldNames(category);
                    DebugLogger.Log($"Calling ExtractFieldsAsync with {fieldNames.Count} fields...");
                    var aiResult = await _ollama.ExtractFieldsAsync(pdfText, category, fieldNames);
                    DebugLogger.Log($"AI returned {aiResult.Count} fields");

                    if (aiResult.Count > 0)
                        return new ExtractionResult { Fields = aiResult, Method = "AI" };

                    DebugLogger.Log("AI returned 0 fields, falling back to regex");
                }
            }
            catch (Exception ex)
            {
                DebugLogger.Log($"AI extraction failed: {ex.GetType().Name}: {ex.Message}");
            }
        }

        var regexResult = FieldExtractor.Extract(pdfText, category);
        DebugLogger.Log($"Regex returned {regexResult.Count} fields");
        return new ExtractionResult { Fields = regexResult, Method = "Regex" };
    }

    private static List<string> GetFieldNames(string category)
    {
        var fields = new List<string>
        {
            "Police Num",
            "Souscripteur",
            "Adresse",
            "Date d'effet",
            "Date d'échéance",
            "Téléphone"
        };

        if (category == "AUTO")
        {
            fields.AddRange(new[] { "Immatriculation", "Marque véhicule" });
        }

        return fields;
    }
}
