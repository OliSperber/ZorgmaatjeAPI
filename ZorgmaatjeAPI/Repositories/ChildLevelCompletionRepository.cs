using _2dRooms.Services;
using ZorgmaatjeAPI.Data.Queries;
using ZorgmaatjeAPI.Models;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Repositories
{
    public class ChildLevelCompletionRepository : SqlService
    {
        private readonly UserIdService _userIdService;

        public ChildLevelCompletionRepository(ConnectionStringService connectionStringService, UserIdService userIdService) : base(connectionStringService)
        {
            _userIdService = userIdService;
        }

        public async Task<IEnumerable<ChildLevelCompletion>> GetAll()
        {
            // Obtaining userId
            var userId = _userIdService.GetUserIdFromToken();

            // Executing GetAllLevelCompletions query
            return await base.QueryAsync<ChildLevelCompletion>(ChildLevelCompletionQueries.GetAllLevelCompletions, new { ChildId = userId });
        }

        public async Task<ChildLevelCompletion?> GetByLevelId(int levelId)
        {
            // Obtaining userId
            var userId = _userIdService.GetUserIdFromToken();

            // Executing GetLevelCompletions query
            return await base.QuerySingleAsync<ChildLevelCompletion>(ChildLevelCompletionQueries.GetLevelCompletion, new { LevelId = levelId, ChildId = userId });
        }

        public async Task RecordCompletion(int levelId, int stickerId)
        {
            // Obtaining userId
            var userId = _userIdService.GetUserIdFromToken();

            // Executing RecordLevelCompletion query
            await base.ExecuteAsync(ChildLevelCompletionQueries.RecordLevelCompletion, new {Id = Guid.NewGuid().ToString(), LevelId = levelId, ChildId = userId, StickerId = stickerId });
        }

        public async Task DeleteCompletion(int levelId)
        {
            // Obtaining userId
            var userId = _userIdService.GetUserIdFromToken();

            // Executing RecordLevelCompletion query
            await base.ExecuteAsync(ChildLevelCompletionQueries.DeleteLevelCompletion, new { LevelId = levelId, ChildId = userId });
        }
    }
}
