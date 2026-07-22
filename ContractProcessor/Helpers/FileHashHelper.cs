using System.Security.Cryptography;
using System.Text;

namespace ContractProcessor.Helpers;

public static class FileHashHelper
{
    public static string ComputeSha256(string filePath)
    {
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(filePath);
        var hash = sha256.ComputeHash(stream);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}
