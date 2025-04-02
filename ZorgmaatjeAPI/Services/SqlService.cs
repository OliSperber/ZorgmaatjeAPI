using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
namespace ZorgmaatjeAPI.Services;

public abstract class SqlService
{
    protected readonly string sqlConnectionString;
    private static readonly HashSet<string> AllowedTables = new()
    {
        "Child", "Guardian", "Level", "ChildLevelCompletions", "DiaryEntries", "Appointments", "Sticker"
    };

    // Constructor that receives the connection string
    protected SqlService(ConnectionStringService connectionStringService)
    {
        // Get the connection string from the injected service
        sqlConnectionString = connectionStringService.GetConnectionString();
    }

    protected virtual IDbConnection CreateConnection()
    {
        return new SqlConnection(sqlConnectionString);
    }

    // Generic method to execute queries (INSERT, UPDATE, DELETE)
    public virtual async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(sql, parameters);
    }

    // Generic method to retrieve a single record
    protected virtual async Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
    }

    // Generic method to retrieve multiple records
    protected virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<T>(sql, parameters);
    }

    // Method to validate allowed tables
    protected virtual void ValidateTableName(string tableName)
    {
        if (!AllowedTables.Contains(tableName))
            throw new ArgumentException("Invalid table name");
    }
}
