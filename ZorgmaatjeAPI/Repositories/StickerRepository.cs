using ZorgmaatjeAPI.Data.Queries;
using ZorgmaatjeAPI.Models;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Repositories;

public class StickerRepository : SqlService
{
    public StickerRepository(ConnectionStringService connectionStringService) : base(connectionStringService)
    {
    }

    public async Task<IEnumerable<Sticker>> GetAll()
    {
        return await base.QueryAsync<Sticker>(StickerQueries.GetAllStickers);
    }

    public async Task<Sticker?> GetById(int stickerId)
    {
        return await base.QuerySingleAsync<Sticker>(StickerQueries.GetStickerById, new { Id = stickerId });
    }
}
