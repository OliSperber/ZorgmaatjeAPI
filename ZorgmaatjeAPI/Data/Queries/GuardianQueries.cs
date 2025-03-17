namespace ZorgmaatjeAPI.Data.Queries;

public static class GuardianQueries
{
    public const string GetGuardianByChildId =
        "SELECT * FROM Guardian WHERE ChildId = @ChildId";

    public const string AddGuardian =
        "INSERT INTO Guardian (Id, UserId, ChildId, FirstName, LastName, Email, Phone, Type) " +
        "VALUES (@Id, @UserId, @ChildId, @FirstName, @LastName, @Email, @Phone, @Type)";

    public const string UpdateGuardian =
        "UPDATE Guardian SET FirstName = @FirstName, LastName = @LastName, Email = @Email, " +
        "Phone = @Phone, Type = @Type WHERE Id = @Id";

    public const string DeleteGuardian =
        "DELETE FROM Guardian WHERE Id = @Id";
}