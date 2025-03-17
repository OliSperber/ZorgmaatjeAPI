namespace ZorgmaatjeAPI.Data.Queries;

public static class ChildQueries
{
    public const string GetChildById =
        "SELECT * FROM Child WHERE UserId = @UserId";

    public const string CreateChild =
        "INSERT INTO Child (UserId, FirstName, LastName, DateOfBirth, DoctorName, CreationDate, TreatmentPath, CharacterId) " +
        "VALUES (@UserId, @FirstName, @LastName, @DateOfBirth, @DoctorName, GETDATE(), @TreatmentPath, @CharacterId)";

    public const string UpdateChild =
        "UPDATE Child SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, " +
        "DoctorName = @DoctorName, TreatmentPath = @TreatmentPath, CharacterId = @CharacterId " +
        "WHERE UserId = @UserId";

    public const string DeleteChild =
        "DELETE FROM Child WHERE UserId = @UserId";
}