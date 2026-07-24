namespace ContractProcessor.Models;

public class AppSettings
{
    public string RootFolder { get; set; } = string.Empty;
    public List<string> Categories { get; set; } = new();
    public bool UseAI { get; set; } = true;
    public string AIModel { get; set; } = "llama3.2";
}
