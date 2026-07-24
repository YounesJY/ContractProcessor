using System.Diagnostics;

namespace ContractProcessor.Services;

public static class DebugLogger
{
    private static readonly string LogPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "debug.log");

    public static void Log(string message)
    {
        try
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(LogPath, line);
            Debug.Write(line);
        }
        catch { /* ignore logging errors */ }
    }

    public static void Clear()
    {
        try { File.WriteAllText(LogPath, string.Empty); } catch { }
    }
}
