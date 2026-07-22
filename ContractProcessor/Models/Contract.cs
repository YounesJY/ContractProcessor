namespace ContractProcessor.Models;

public class Contract
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string FileHash { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; } = DateTime.Now;
    public string ExtractedData { get; set; } = "{}";
    public string SelectedFields { get; set; } = "[]";
    public string ProcessingStatus { get; set; } = "Pending";
}
