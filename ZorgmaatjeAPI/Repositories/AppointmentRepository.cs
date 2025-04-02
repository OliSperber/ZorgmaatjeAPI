using ZorgmaatjeAPI.Data.Queries;
using ZorgmaatjeAPI.Models;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Repositories
{
    public class AppointmentRepository : SqlService
    {
        private readonly UserIdService _userIdService;

        public AppointmentRepository(ConnectionStringService connectionStringService, UserIdService userIdService) : base(connectionStringService)
        {
            _userIdService = userIdService;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            var userId = _userIdService.GetUserIdFromToken();

            return await base.QueryAsync<Appointment>(AppointmentQueries.GetAppointmentsByChildId, new { ChildId = userId });
        }

        public async Task<Guardian?> GetByIdAsync(string id)
        {
            return await base.QuerySingleAsync<Guardian>(AppointmentQueries.GetAppointmentById, new { Id = id });
        }

        public async Task<Appointment?> CreateAsync(Appointment appointment)
        {
            var userId = _userIdService.GetUserIdFromToken();

            appointment.ChildId = userId!;
            appointment.Id = Guid.NewGuid().ToString();

            await base.ExecuteAsync(AppointmentQueries.CreateAppointment, appointment);

            return appointment;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            await base.ExecuteAsync(AppointmentQueries.UpdateAppointment, appointment);
        }

        public async Task DeleteAsync(string id)
        {
            await base.ExecuteAsync(AppointmentQueries.DeleteAppointment, new { Id = id });
        }
    }
}
