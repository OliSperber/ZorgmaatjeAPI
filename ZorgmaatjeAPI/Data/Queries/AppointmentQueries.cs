namespace ZorgmaatjeAPI.Data.Queries;

public static class AppointmentQueries
{
    public const string GetAppointmentsByChildId =
        "SELECT * FROM Appointments WHERE ChildId = @ChildId";

    public const string CreateAppointment =
        "INSERT INTO Appointments (Id, ChildId, DoctorName, Date, AppointmentName) " +
        "VALUES (@Id, @ChildId, @DoctorName, @Date, @AppointmentName)";

    public const string UpdateAppointment =
        "UPDATE Appointments SET DoctorName = @DoctorName, Date = @Date, " +
        "AppointmentName = @AppointmentName WHERE Id = @Id";

    public const string DeleteAppointment =
        "DELETE FROM Appointments WHERE Id = @Id";
}