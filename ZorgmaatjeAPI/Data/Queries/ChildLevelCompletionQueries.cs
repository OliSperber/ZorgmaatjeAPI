namespace ZorgmaatjeAPI.Data.Queries;

public static class ChildLevelCompletionQueries
{
    public const string GetAllLevelCompletions =
        "SELECT * FROM ChildLevelCompletions WHERE ChildId = @ChildId";

    public const string GetLevelCompletion =
        "SELECT * FROM ChildLevelCompletions WHERE LevelId = @LevelId AND ChildId = @ChildId";

    public const string RecordLevelCompletion =
        "IF NOT EXISTS (SELECT 1 FROM ChildLevelCompletions WHERE LevelId = @LevelId AND ChildId = @ChildId) " +
        "INSERT INTO ChildLevelCompletions (Id, LevelId, ChildId, CompletionDate, StickerId) " +
        "VALUES (@Id, @LevelId, @ChildId, GETDATE(), @StickerId)";

    public const string DeleteLevelCompletion =
        "DELETE FROM ChildLevelCompletions WHERE LevelId = @LevelId AND ChildId = @ChildId";
}
