using Microsoft.AspNetCore.Mvc;
using ZorgmaatjeAPI.Services;
using ZorgmaatjeAPI.Repositories;
using ZorgmaatjeAPI.Models;

namespace ZorgmaatjeAPI.Controllers;

[Route("api/diary")]
[ApiController]
public class DiaryEntryController : ControllerBase
{
    private readonly DiaryEntryRepository _diaryEntryRepository;
    private readonly AuthorizationService _authorizationService;

    // Constructor for dependency injection
    public DiaryEntryController(DiaryEntryRepository diaryEntryRepository, AuthorizationService authorizationService)
    {
        _diaryEntryRepository = diaryEntryRepository;
        _authorizationService = authorizationService;
    }


    // GET: api/diary?childId={childId}
    [HttpGet]
    public async Task<IActionResult> GetDiaryEntries()
    {
        IEnumerable<DiaryEntry> diaryEntries = await _diaryEntryRepository.GetAll(); 

        return Ok(diaryEntries);
    }

    // POST: api/diary
    [HttpPost]
    public async Task<IActionResult> CreateDiaryEntry([FromBody] DiaryEntry diaryEntry)
    {
        // Checks if ModelState is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Creating diary entry
        try
        {
            var createdEntry = await _diaryEntryRepository.CreateAsync(diaryEntry);

            return Created(string.Empty, createdEntry);
        }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex}"); }
    }

    // PUT: api/diary/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiaryEntry([FromBody] DiaryEntry diaryEntry)
    {
        // Checks if ModelState is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if DiaryEntry Exists
        if (await _diaryEntryRepository.GetByIdAsync(diaryEntry.Id) == null)
            return NoContent();

        // Check if user is authorized to modify the Diary Entry
        if (!await _authorizationService.IsUserAuthorizedForEntityAsync("DiaryEntries", diaryEntry.Id))
            return Forbid();

        // Updating diary entry
        try { await _diaryEntryRepository.UpdateAsync(diaryEntry); }
        catch(Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex}"); }

        return Ok(diaryEntry);
    }

    // DELETE: api/diary/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiaryEntry(string id)
    {
        // Check if DiaryEntry Exists
        if (await _diaryEntryRepository.GetByIdAsync(id) == null)
            return NoContent();

        // Check if user is authorized to modify the Diary Entry
        if (!await _authorizationService.IsUserAuthorizedForEntityAsync("DiaryEntries", id))
            return Forbid();

        // Deleting diary
        try { await _diaryEntryRepository.DeleteAsync(id); }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex}"); }

        return NoContent();
    }
}
