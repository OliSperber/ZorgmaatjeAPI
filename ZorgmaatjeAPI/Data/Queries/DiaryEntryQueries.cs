namespace ZorgmaatjeAPI.Data.Queries;

public static class DiaryEntryQueries
{
    public const string GetDiaryEntriesByChildId =
        "SELECT * FROM DiaryEntries WHERE ChildId = @ChildId";

    public const string CreateDiaryEntry =
        "INSERT INTO DiaryEntries (Id, ChildId, Content, StickerId, Date) " +
        "VALUES (@Id, @ChildId, @Content, @StickerId, GETDATE())";

    public const string UpdateDiaryEntry =
        "UPDATE DiaryEntries SET Content = @Content, StickerId = @StickerId WHERE Id = @Id";

    public const string DeleteDiaryEntry =
        "DELETE FROM DiaryEntries WHERE Id = @Id";
}