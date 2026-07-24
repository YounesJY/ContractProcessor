using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContractProcessor.Services;

public class OpenRouterService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private readonly string _model;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public OpenRouterService(string apiKey, string model = "meta-llama/llama-3.1-8b-instruct:free")
    {
        _apiKey = apiKey;
        _model = model;
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://openrouter.ai/api/v1/"),
            Timeout = TimeSpan.FromMinutes(3)
        };
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        _http.DefaultRequestHeaders.Add("HTTP-Referer", "https://github.com/YounesJY/ContractProcessor");
        _http.DefaultRequestHeaders.Add("X-Title", "ContractProcessor");
    }

    public async Task<bool> IsAvailableAsync()
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
            return false;

        try
        {
            var response = await _http.GetAsync("models");
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
            var response = await _http.GetAsync("models");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<OpenRouterModelsResponse>(JsonOptions);
            return data?.Data?.Select(m => m.Id).ToList() ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    public async Task<Dictionary<string, string>> ExtractFieldsAsync(string pdfText, string category, List<string> fieldNames)
    {
        var prompt = BuildPrompt(pdfText, category, fieldNames);

        var request = new OpenRouterChatRequest
        {
            Model = _model,
            Messages = new[]
            {
                new OpenRouterMessage { Role = "system", Content = "You are an insurance document data extractor. Return ONLY valid JSON with exact field names." },
                new OpenRouterMessage { Role = "user", Content = prompt }
            },
            Temperature = 0.1,
            MaxTokens = 2000,
            ResponseFormat = new { type = "json_object" }
        };

        DebugLogger.Log($"OpenRouter request: model={_model}, url=chat/completions");
        var response = await _http.PostAsJsonAsync("chat/completions", request, JsonOptions);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            DebugLogger.Log($"OpenRouter error: {response.StatusCode} - {errorContent}");
            response.EnsureSuccessStatusCode();
        }

        var result = await response.Content.ReadFromJsonAsync<OpenRouterChatResponse>(JsonOptions);
        var content = result?.Choices?.FirstOrDefault()?.Message?.Content ?? string.Empty;

        return ParseJsonResponse(content, fieldNames);
    }

    private static string BuildPrompt(string pdfText, string category, List<string> fieldNames)
    {
        var fieldList = string.Join(", ", fieldNames.Select(f => $"\"{f}\""));
        var truncatedText = pdfText.Length > 6000 ? pdfText[..6000] + "..." : pdfText;

        return $@"Extract data from a Moroccan {category} insurance contract (Allianz/Sanlam).

Return EXACTLY this JSON structure with these EXACT keys:
{{
  ""Police Num"": ""value or null"",
  ""Souscripteur"": ""value or null"",
  ""Adresse"": ""value or null"",
  ""Date d'effet"": ""DD/MM/YYYY or null"",
  ""Date d'échéance"": ""DD/MM/YYYY or null"",
  ""Téléphone"": ""digits only or null""
}}

Rules:
- Use EXACT key names above — do NOT rename or translate
- If field not found, use null (not empty string, not ""null"")
- Date d'effet = START DATE of contract (usually earlier date)
- Date d'échéance = END DATE of contract (usually later date)
- Phone: digits only, no spaces, no +212, no parentheses
- Names: UPPERCASE
- Return ONLY the JSON object — no markdown, no explanation, no extra text

Contract text:
{truncatedText}";
    }

    private static Dictionary<string, string> ParseJsonResponse(string responseText, List<string> fieldNames)
    {
        var result = new Dictionary<string, string>();

        responseText = responseText.Trim();

        // Strip markdown code blocks
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
                    // Exact match first
                    if (parsed.TryGetValue(field, out var value))
                    {
                        var str = FormatValue(value);
                        if (!string.IsNullOrEmpty(str))
                            result[field] = str;
                        continue;
                    }

                    // Fuzzy match (case/accent insensitive)
                    var normField = Normalize(field);
                    var match = parsed.FirstOrDefault(kvp => Normalize(kvp.Key) == normField);
                    if (match.Key != null)
                    {
                        var str = FormatValue(match.Value);
                        if (!string.IsNullOrEmpty(str))
                            result[field] = str;
                    }
                }
            }
        }
        catch (JsonException)
        {
            // Malformed JSON — return empty to trigger fallback
        }

        return result;
    }

    private static string FormatValue(JsonElement value)
    {
        var str = value.ValueKind switch
        {
            JsonValueKind.String => value.GetString() ?? string.Empty,
            JsonValueKind.Number => value.ToString(),
            JsonValueKind.Null => string.Empty,
            _ => value.ToString()
        };

        if (string.IsNullOrWhiteSpace(str) || str.Equals("null", StringComparison.OrdinalIgnoreCase))
            return string.Empty;

        return str.Trim();
    }

    private static string Normalize(string s)
    {
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

#region OpenRouter API Models

internal class OpenRouterChatRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("messages")]
    public OpenRouterMessage[] Messages { get; set; } = Array.Empty<OpenRouterMessage>();

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; }

    [JsonPropertyName("response_format")]
    public object ResponseFormat { get; set; } = new { type = "json_object" };
}

internal class OpenRouterMessage
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}

internal class OpenRouterChatResponse
{
    [JsonPropertyName("choices")]
    public OpenRouterChoice[] Choices { get; set; } = Array.Empty<OpenRouterChoice>();
}

internal class OpenRouterChoice
{
    [JsonPropertyName("message")]
    public OpenRouterMessage Message { get; set; } = new();
}

internal class OpenRouterModelsResponse
{
    [JsonPropertyName("data")]
    public OpenRouterModel[] Data { get; set; } = Array.Empty<OpenRouterModel>();
}

internal class OpenRouterModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}

#endregion