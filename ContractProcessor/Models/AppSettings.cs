namespace ContractProcessor.Models;

public class AppSettings
{
    public string RootFolder { get; set; } = string.Empty;
    public List<string> Categories { get; set; } = new();
}
