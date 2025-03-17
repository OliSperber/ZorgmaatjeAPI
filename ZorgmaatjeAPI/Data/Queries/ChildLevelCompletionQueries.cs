namespace ZorgmaatjeAPI.Data.Queries;

public static class ChildLevelCompletionQueries
{
    public const string GetLevelsCompletedByChild =
        "SELECT * FROM ChildLevelCompletions WHERE ChildId = @ChildId";

    public const string GetLevelCompletion =
        "SELECT * FROM ChildLevelCompletions WHERE Id = @CompletionId";

    public const string RecordLevelCompletion =
        "INSERT INTO ChildLevelCompletions (Id, LevelId, ChildId, CompletionDate, StickerId) " +
        "VALUES (@Id, @LevelId, @ChildId, GETDATE(), @StickerId)";

    public const string DeleteLevelCompletion =
        "DELETE FROM ChildLevelCompletions WHERE Id = @CompletionId";
}