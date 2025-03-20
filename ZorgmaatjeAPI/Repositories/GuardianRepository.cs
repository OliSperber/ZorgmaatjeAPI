using _2dRooms.Services;
using ZorgmaatjeAPI.Data.Queries;
using ZorgmaatjeAPI.Models;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Repositories
{
    public class GuardianRepository : SqlService
    {
        private readonly UserIdService _userIdService;

        public GuardianRepository(ConnectionStringService connectionStringService, UserIdService userIdService) : base(connectionStringService)
        {
            _userIdService = userIdService;
        }

        public async Task<Guardian?> GetGuardian()
        {
            var userId = _userIdService.GetUserIdFromToken();

            return await base.QuerySingleAsync<Guardian>(GuardianQueries.GetGuardianByChildId, new { ChildId = userId });
        }

        public async Task<Guardian?> GetByIdAsync(string id)
        {
            return await base.QuerySingleAsync<Guardian>(GuardianQueries.GetGuardianById, new { Id = id });
        }

        public async Task<Guardian?> CreateAsync(Guardian guardian)
        {
            if (await GetGuardian() != null)
                throw new Exception("No more then 1 parent can be created per child");

            var userId = _userIdService.GetUserIdFromToken();

            guardian.ChildId = userId!;
            guardian.UserId = userId!;
            guardian.Id = Guid.NewGuid().ToString();

            await base.ExecuteAsync(GuardianQueries.AddGuardian, guardian);

            return guardian;
        }

        public async Task UpdateAsync(Guardian guardian)
        {
            await base.ExecuteAsync(GuardianQueries.UpdateGuardian, guardian);
        }

        public async Task DeleteAsync(string id)
        {
            await base.ExecuteAsync(GuardianQueries.DeleteGuardian, new { Id = id });
        }
    }
}
