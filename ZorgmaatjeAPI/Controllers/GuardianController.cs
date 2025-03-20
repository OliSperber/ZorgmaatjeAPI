using Microsoft.AspNetCore.Mvc;
using ZorgmaatjeAPI.Services;
using ZorgmaatjeAPI.Repositories;
using ZorgmaatjeAPI.Models;
using System.Threading.Tasks;

namespace ZorgmaatjeAPI.Controllers;

[ApiController]
[Route("api/guardians")]
public class GuardianController : ControllerBase
{
    private readonly GuardianRepository _guardianRepository;
    private readonly AuthorizationService _authorizationService;

    // Constructor for dependency injection
    public GuardianController(GuardianRepository guardianRepository, AuthorizationService authorizationService)
    {
        _guardianRepository = guardianRepository;
        _authorizationService = authorizationService;
    }

    // GET: api/guardians
    [HttpGet]
    public async Task<IActionResult> GetGuardian()
    {
        // Querying for the guardian
        Guardian? guardian = await _guardianRepository.GetGuardian();

        // Validating Guardian isnt null
        if (guardian == null)
            return NotFound();

        return Ok(guardian);
    }

    // POST: api/guardians
    [HttpPost]
    public async Task<IActionResult> CreateGuardian([FromBody] Guardian guardian)
    {
        // Checks if ModelState is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Creating guardian
        try
        {
            var createdEntry = await _guardianRepository.CreateAsync(guardian);

            return Created(string.Empty, createdEntry);
        }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex.Message}"); }
    }

    // PUT: api/guardians
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGuardian([FromBody] Guardian guardian)
    {
        // Checks if ModelState is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if Guardian Exists
        if (await _guardianRepository.GetByIdAsync(guardian.Id) == null)
            return NoContent();

        // Check if user is authorized to modify the Guardian
        if (!await _authorizationService.IsUserAuthorizedForEntityAsync("Guardian", guardian.Id))
            return Forbid();

        try { await _guardianRepository.UpdateAsync(guardian); }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex.Message}"); }

        return Ok(guardian);
    }

    // DELETE: api/guardians/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGuardian(string id)
    {
        // Check if Guardian Exists
        if (await _guardianRepository.GetByIdAsync(id) == null)
            return NoContent();

        // Check if user is authorized to modify the Guardian
        if (!await _authorizationService.IsUserAuthorizedForEntityAsync("Guardian", id))
            return Forbid();

        // Deleting Guardian
        try { await _guardianRepository.DeleteAsync(id); }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex.Message}"); }

        return NoContent();
    }
}
