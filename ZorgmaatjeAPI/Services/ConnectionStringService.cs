namespace _2dRooms.Services;

public class ConnectionStringService
{
    private readonly string _connectionString;

    public ConnectionStringService(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("SqlConnectionString") ?? throw new ArgumentNullException("Missing connection string");
    }

    public string GetConnectionString() => _connectionString;
}
