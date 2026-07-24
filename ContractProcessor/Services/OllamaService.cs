using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContractProcessor.Services;

public class OllamaService
{
    private readonly HttpClient _http;
    private readonly string _model;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public OllamaService(string model = "llama3.2")
    {
        _model = model;
        _http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:11434"),
            Timeout = TimeSpan.FromMinutes(5)
        };
    }

    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            var response = await _http.GetAsync("/api/tags");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<string>> GetModelsAsync()
    {
        try
        {
            var response = await _http.GetAsync("/api/tags");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<OllamaModelsResponse>();
            return data?.Models?.Select(m => m.Name)?.ToList() ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    public async Task<Dictionary<string, string>> ExtractFieldsAsync(string pdfText, string category, List<string> fieldNames)
    {
        var prompt = BuildPrompt(pdfText, category, fieldNames);
        DebugLogger.Log($"Ollama prompt length: {prompt.Length}");

        var request = new OllamaGenerateRequest
        {
            Model = _model,
            Prompt = prompt,
            Stream = false,
            Options = new OllamaOptions
            {
                Temperature = 0.1,
                TopP = 0.9
            }
        };

        DebugLogger.Log($"Sending request to Ollama (model={_model})...");
        var response = await _http.PostAsJsonAsync("/api/generate", request, JsonOptions);
        DebugLogger.Log($"Ollama response status: {response.StatusCode}");
        response.EnsureSuccessStatusCode();

        // Read as string first to preserve UTF-8 encoding
        var rawJson = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OllamaGenerateResponse>(rawJson, JsonOptions);
        var responseText = result?.Response ?? string.Empty;
        DebugLogger.Log($"Ollama response length: {responseText.Length}");
        DebugLogger.Log($"Ollama response (first 500): {responseText[..Math.Min(500, responseText.Length)]}");

        var parsed = ParseJsonResponse(responseText, fieldNames);
        DebugLogger.Log($"Parsed {parsed.Count} fields from response");
        return parsed;
    }

    private static string BuildPrompt(string pdfText, string category, List<string> fieldNames)
    {
        var truncatedText = pdfText.Length > 4000 ? pdfText[..4000] + "..." : pdfText;

        return $@"Extract data from a Moroccan {category} insurance contract (Allianz/Sanlam).

Return EXACTLY this JSON with these EXACT keys:
{{
  ""Police Num"": ""value or null"",
  ""Souscripteur"": ""value or null"",
  ""Adresse"": ""value or null"",
  ""Date d'effet"": ""DD/MM/YYYY or null"",
  ""Date d'échéance"": ""DD/MM/YYYY or null"",
  ""Téléphone"": ""digits only or null""
}}

Rules:
- Use EXACT key names, do NOT rename
- Dates MUST be short: DD/MM/YYYY only, NOT paragraphs of text
- Names in UPPERCASE
- Phone: digits only, no spaces
- Return ONLY the raw JSON, no explanation, no code blocks

Text:
{truncatedText}";
    }

    private static Dictionary<string, string> ParseJsonResponse(string responseText, List<string> fieldNames)
    {
        var result = new Dictionary<string, string>();

        responseText = responseText.Trim();

        // Strip markdown code blocks if present
        responseText = System.Text.RegularExpressions.Regex.Replace(responseText, @"```(?:json)?\s*", "");
        responseText = System.Text.RegularExpressions.Regex.Replace(responseText, @"```\s*", "");

        var jsonStart = responseText.IndexOf('{');
        var jsonEnd = responseText.LastIndexOf('}');
        if (jsonStart >= 0 && jsonEnd > jsonStart)
        {
            responseText = responseText[jsonStart..(jsonEnd + 1)];
        }

        try
        {
            var parsed = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseText, JsonOptions);
            if (parsed != null)
            {
                foreach (var field in fieldNames)
                {
                    // Try exact match first
                    if (parsed.TryGetValue(field, out var value))
                    {
                        var strValue = FormatJsonValue(value);
                        if (!string.IsNullOrEmpty(strValue))
                            result[field] = strValue;
                        continue;
                    }

                    // Try fuzzy match (case-insensitive, accent-insensitive)
                    var normalizedField = NormalizeForMatch(field);
                    var match = parsed.FirstOrDefault(kvp => NormalizeForMatch(kvp.Key) == normalizedField);
                    if (match.Key != null)
                    {
                        var strValue = FormatJsonValue(match.Value);
                        if (!string.IsNullOrEmpty(strValue))
                            result[field] = strValue;
                    }
                }
            }
        }
        catch (JsonException)
        {
            // LLM returned malformed JSON — return empty to trigger fallback
        }

        return result;
    }

    private static string FormatJsonValue(JsonElement value)
    {
        var strValue = value.ValueKind switch
        {
            JsonValueKind.String => value.GetString() ?? string.Empty,
            JsonValueKind.Number => value.ToString(),
            JsonValueKind.Null => string.Empty,
            _ => value.ToString()
        };

        if (string.IsNullOrWhiteSpace(strValue) || strValue.ToLower() == "null")
            return string.Empty;

        return strValue.Trim();
    }

    private static string NormalizeForMatch(string s)
    {
        // Remove accents, lowercase, remove spaces/special chars
        var normalized = s.Normalize(System.Text.NormalizationForm.FormD);
        var sb = new System.Text.StringBuilder();
        foreach (var c in normalized)
        {
            var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        return sb.ToString()
            .Normalize(System.Text.NormalizationForm.FormC)
            .ToLowerInvariant()
            .Replace(" ", "")
            .Replace("'", "")
            .Replace("'", "")
            .Replace("°", "")
            .Replace("é", "e")
            .Replace("è", "e")
            .Replace("ê", "e")
            .Replace("à", "a")
            .Replace("â", "a")
            .Replace("ô", "o")
            .Replace("û", "u")
            .Replace("ü", "u")
            .Replace("î", "i")
            .Replace("ï", "i");
    }
}

#region Ollama API Models

internal class OllamaGenerateRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    [JsonPropertyName("stream")]
    public bool Stream { get; set; }

    [JsonPropertyName("options")]
    public OllamaOptions? Options { get; set; }
}

internal class OllamaOptions
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("top_p")]
    public double TopP { get; set; }
}

internal class OllamaGenerateResponse
{
    [JsonPropertyName("response")]
    public string Response { get; set; } = string.Empty;

    [JsonPropertyName("done")]
    public bool Done { get; set; }
}

internal class OllamaModelsResponse
{
    [JsonPropertyName("models")]
    public List<OllamaModel>? Models { get; set; }
}

internal class OllamaModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

#endregion
