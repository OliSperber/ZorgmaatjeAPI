namespace ZorgmaatjeAPI.Data.Queries;

public static class LevelQueries
{
    public const string GetAllLevels =
        "SELECT * FROM Level";

    public const string GetLevelById =
        "SELECT * FROM Level WHERE Id = @Id";
}
