using _2dRooms.Services;
using ZorgmaatjeAPI.Data.Queries;
using ZorgmaatjeAPI.Models;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Repositories;

public class LevelRepository : SqlService
{

    public LevelRepository(ConnectionStringService connectionStringService) : base(connectionStringService)
    {
    }

    public async Task<IEnumerable<Level>> GetAll()
    {
        return await base.QueryAsync<Level>(LevelQueries.GetAllLevels);
    }

    public async Task<Level?> GetById(int stickerId)
    {
        return await base.QuerySingleAsync<Level>(LevelQueries.GetLevelById, new { Id = stickerId });
    }
}
