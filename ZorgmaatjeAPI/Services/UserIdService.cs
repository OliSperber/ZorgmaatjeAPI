using System.Security.Claims;

namespace ZorgmaatjeAPI.Services;

public class UserIdService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserIdService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserIdFromToken()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new Exception("userId not found in token");

        return userId;
    }
}
