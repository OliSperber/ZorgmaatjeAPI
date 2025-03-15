using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace _2dRooms.Services;

public abstract class SqlService
{
    protected readonly string sqlConnectionString;

    // Constructor that receives the connection string
    protected SqlService(ConnectionStringService connectionStringService)
    {
        // Get the connection string from the injected service
        sqlConnectionString = connectionStringService.GetConnectionString();
    }

    protected IDbConnection CreateConnection()
    {
        return new SqlConnection(sqlConnectionString);
    }

    // Generic method to execute queries (INSERT, UPDATE, DELETE)
    protected async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(sql, parameters);
    }

    // Generic method to retrieve a single record
    protected async Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
    }

    // Generic method to retrieve multiple records
    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<T>(sql, parameters);
    }
}
