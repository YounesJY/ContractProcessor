using System.Text.Json;

namespace ContractProcessor.Data;

public class DatabaseInitializer
{
    private readonly string _dbPath;

    public DatabaseInitializer(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void Initialize()
    {
        var directory = Path.GetDirectoryName(_dbPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        using var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={_dbPath}");
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS contracts (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                FileName        TEXT NOT NULL,
                FilePath        TEXT NOT NULL,
                Category        TEXT NOT NULL DEFAULT 'Unknown',
                FileHash        TEXT UNIQUE NOT NULL,
                UploadDate      DATETIME DEFAULT CURRENT_TIMESTAMP,
                ExtractedData   TEXT DEFAULT '{}',
                SelectedFields  TEXT DEFAULT '[]',
                ProcessingStatus TEXT DEFAULT 'Pending'
            );

            CREATE INDEX IF NOT EXISTS idx_category ON contracts(Category);

            CREATE TABLE IF NOT EXISTS categories (
                Id   INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT UNIQUE NOT NULL
            );

            INSERT OR IGNORE INTO categories (Name) VALUES ('AT'), ('AUTO'), ('MRH');

            CREATE TABLE IF NOT EXISTS settings (
                Key   TEXT PRIMARY KEY,
                Value TEXT NOT NULL
            );
        ";
        cmd.ExecuteNonQuery();
    }
}
