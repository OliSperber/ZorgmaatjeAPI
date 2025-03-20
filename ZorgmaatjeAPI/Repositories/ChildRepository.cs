using System.Data.Common;
using System.Security.Claims;
using _2dRooms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ZorgmaatjeAPI.Data.Queries;
using ZorgmaatjeAPI.Models;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Repositories;

public class ChildRepository : SqlService
{
    private readonly UserIdService _userIdService;

    public ChildRepository(ConnectionStringService connectionStringService, UserIdService userIdService) : base(connectionStringService)
    {
        _userIdService = userIdService;
    }

    public async Task<Child?> GetChild()
    {
        // Obtaining userId
        var userId = _userIdService.GetUserIdFromToken();

        // Executing GetChild query
        return await base.QuerySingleAsync<Child>(ChildQueries.GetChildById, new { UserId = userId });
    }

    public async Task<Child> UpdateAsync(Child child)
    {
        // Obtaining userId and add to child
        var userId = _userIdService.GetUserIdFromToken();
        child.UserId = userId!;

        // Executing Update query
        await base.ExecuteAsync(ChildQueries.UpdateChild, child);
        return child;
    }

    public async Task DeleteAsync()
    {
        // Obtaining userId
        var userId = _userIdService.GetUserIdFromToken();

        // Executing Delete query
        await base.ExecuteAsync(ChildQueries.DeleteChild, new { UserId = userId });
    }

    public async Task<Child?> CreateAsync(Child child)
    {
        // Obtaining userId and add to child
        var userId = _userIdService.GetUserIdFromToken();
        child.UserId = userId!;

        // Executing Create query
        await base.ExecuteAsync(ChildQueries.CreateChild, child);
        return child;
    }
}

