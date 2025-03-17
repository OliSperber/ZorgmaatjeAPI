namespace ZorgmaatjeAPI.Data.Queries;

public static class StickerQueries
{
    public const string GetAllStickers =
        "SELECT * FROM Sticker";

    public const string GetStickerById =
        "SELECT * FROM Sticker WHERE Id = @Id";
}