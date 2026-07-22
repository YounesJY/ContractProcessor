using System.Text.Json;
using ContractProcessor.Models;
using Microsoft.Data.Sqlite;

namespace ContractProcessor.Data;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
    }

    private SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    // --- Contracts ---

    public bool DuplicateExists(string fileHash)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM contracts WHERE FileHash = @hash";
        cmd.Parameters.AddWithValue("@hash", fileHash);
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public void InsertContract(Contract contract)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO contracts (FileName, FilePath, Category, FileHash, UploadDate, ExtractedData, SelectedFields, ProcessingStatus)
            VALUES (@fileName, @filePath, @category, @fileHash, @uploadDate, @extractedData, @selectedFields, @status)";
        cmd.Parameters.AddWithValue("@fileName", contract.FileName);
        cmd.Parameters.AddWithValue("@filePath", contract.FilePath);
        cmd.Parameters.AddWithValue("@category", contract.Category);
        cmd.Parameters.AddWithValue("@fileHash", contract.FileHash);
        cmd.Parameters.AddWithValue("@uploadDate", contract.UploadDate.ToString("o"));
        cmd.Parameters.AddWithValue("@extractedData", contract.ExtractedData);
        cmd.Parameters.AddWithValue("@selectedFields", contract.SelectedFields);
        cmd.Parameters.AddWithValue("@status", contract.ProcessingStatus);
        cmd.ExecuteNonQuery();
    }

    public void UpdateExtractedData(int id, string extractedData, string status)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE contracts SET ExtractedData = @data, ProcessingStatus = @status WHERE Id = @id";
        cmd.Parameters.AddWithValue("@data", extractedData);
        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void UpdateSelectedFields(int id, string selectedFields)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE contracts SET SelectedFields = @fields WHERE Id = @id";
        cmd.Parameters.AddWithValue("@fields", selectedFields);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void UpdateCategory(int id, string category)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE contracts SET Category = @category WHERE Id = @id";
        cmd.Parameters.AddWithValue("@category", category);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<Contract> GetAllContracts(string? categoryFilter = null)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();

        if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "All")
        {
            cmd.CommandText = "SELECT * FROM contracts WHERE Category = @category ORDER BY UploadDate DESC";
            cmd.Parameters.AddWithValue("@category", categoryFilter);
        }
        else
        {
            cmd.CommandText = "SELECT * FROM contracts ORDER BY UploadDate DESC";
        }

        var contracts = new List<Contract>();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            contracts.Add(new Contract
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FileName = reader.GetString(reader.GetOrdinal("FileName")),
                FilePath = reader.GetString(reader.GetOrdinal("FilePath")),
                Category = reader.GetString(reader.GetOrdinal("Category")),
                FileHash = reader.GetString(reader.GetOrdinal("FileHash")),
                UploadDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("UploadDate"))),
                ExtractedData = reader.GetString(reader.GetOrdinal("ExtractedData")),
                SelectedFields = reader.GetString(reader.GetOrdinal("SelectedFields")),
                ProcessingStatus = reader.GetString(reader.GetOrdinal("ProcessingStatus"))
            });
        }
        return contracts;
    }

    public Contract? GetContract(int id)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM contracts WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Contract
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FileName = reader.GetString(reader.GetOrdinal("FileName")),
                FilePath = reader.GetString(reader.GetOrdinal("FilePath")),
                Category = reader.GetString(reader.GetOrdinal("Category")),
                FileHash = reader.GetString(reader.GetOrdinal("FileHash")),
                UploadDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("UploadDate"))),
                ExtractedData = reader.GetString(reader.GetOrdinal("ExtractedData")),
                SelectedFields = reader.GetString(reader.GetOrdinal("SelectedFields")),
                ProcessingStatus = reader.GetString(reader.GetOrdinal("ProcessingStatus"))
            };
        }
        return null;
    }

    public void DeleteContract(int id)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM contracts WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    // --- Categories ---

    public List<string> GetCategories()
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Name FROM categories ORDER BY Name";

        var categories = new List<string>();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            categories.Add(reader.GetString(0));
        return categories;
    }

    public void AddCategory(string name)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT OR IGNORE INTO categories (Name) VALUES (@name)";
        cmd.Parameters.AddWithValue("@name", name);
        cmd.ExecuteNonQuery();
    }

    public void DeleteCategory(string name)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM categories WHERE Name = @name";
        cmd.Parameters.AddWithValue("@name", name);
        cmd.ExecuteNonQuery();
    }

    // --- Settings ---

    public string? GetSetting(string key)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Value FROM settings WHERE Key = @key";
        cmd.Parameters.AddWithValue("@key", key);
        var result = cmd.ExecuteScalar();
        return result?.ToString();
    }

    public void SetSetting(string key, string value)
    {
        using var conn = CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT OR REPLACE INTO settings (Key, Value) VALUES (@key, @value)";
        cmd.Parameters.AddWithValue("@key", key);
        cmd.Parameters.AddWithValue("@value", value);
        cmd.ExecuteNonQuery();
    }
}
