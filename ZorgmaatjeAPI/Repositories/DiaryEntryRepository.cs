using System.Data.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ZorgmaatjeAPI.Data.Queries;
using ZorgmaatjeAPI.Models;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Repositories;

public class DiaryEntryRepository : SqlService
{
    private readonly UserIdService _userIdService;

    public DiaryEntryRepository(ConnectionStringService connectionStringService, UserIdService userIdService) : base(connectionStringService)
    {
        _userIdService = userIdService;
    }

    public async Task<DiaryEntry?> GetByIdAsync(string id)
    {
        return await base.QuerySingleAsync<DiaryEntry>(DiaryEntryQueries.GetDiaryEntryById, new { Id = id });
    }

    public async Task UpdateAsync(DiaryEntry diaryEntry)
    {
        await base.ExecuteAsync(DiaryEntryQueries.UpdateDiaryEntry, diaryEntry);
    }

    public async Task DeleteAsync(string id)
    {
        await base.ExecuteAsync(DiaryEntryQueries.DeleteDiaryEntry, new { Id = id });
    }

    public async Task<DiaryEntry?> CreateAsync(DiaryEntry diaryEntry)
    {
        var userId = _userIdService.GetUserIdFromToken();

        diaryEntry.ChildId = userId!;
        diaryEntry.Id = Guid.NewGuid().ToString();
        diaryEntry.Date = DateTime.Now;

        await base.ExecuteAsync(DiaryEntryQueries.CreateDiaryEntry, diaryEntry);

        return diaryEntry;
    }

    public async Task<IEnumerable<DiaryEntry>> GetAll()
    {
        var userId = _userIdService.GetUserIdFromToken();

        return await base.QueryAsync<DiaryEntry>(DiaryEntryQueries.GetDiaryEntriesByChildId, new { ChildId = userId });
    }
}

