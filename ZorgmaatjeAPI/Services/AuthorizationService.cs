using System.Security.Claims;
using _2dRooms.Services;

namespace ZorgmaatjeAPI.Services;

public class AuthorizationService : SqlService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationService(IHttpContextAccessor httpContextAccessor, ConnectionStringService connectionStringService)
        : base(connectionStringService)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> IsUserAuthorizedForEntityAsync(string entityTable, string entityId)
    {
        ValidateTableName(entityTable); // Ensure the table name is valid

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return false;

        string query = $@"SELECT COUNT(*) FROM {entityTable} WHERE Id = @EntityId AND ChildId = @UserId";

        var count = await QuerySingleAsync<int>(query, new { EntityId = entityId, UserId = userId });
        return count > 0;
    }
}
