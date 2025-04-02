namespace ZorgmaatjeAPI.Services;

public class ConnectionStringService
{
    private readonly string _connectionString;

    public ConnectionStringService(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("SqlConnectionString") ?? throw new ArgumentNullException("Missing connection string");
    }

    public ConnectionStringService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public string GetConnectionString() => _connectionString;
}
