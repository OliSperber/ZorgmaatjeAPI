using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZorgmaatjeAPI.Models;
using ZorgmaatjeAPI.Repositories;

namespace ZorgmaatjeAPI.Controllers;

[Route("api/child/levels")]
[ApiController]
public class ChildLevelCompletionController : ControllerBase
{
    private readonly ChildLevelCompletionRepository _childLevelCompletionRepository;

    // Constructor for dependency injection
    public ChildLevelCompletionController(ChildLevelCompletionRepository childLevelCompletionRepository)
    {
        _childLevelCompletionRepository = childLevelCompletionRepository;
    }

    // Get all levels completed by a child
    [HttpGet]
    public async Task<IActionResult> GetAllLevelCompletions()
    {
        // Query for all completions in DB
        IEnumerable<ChildLevelCompletion> levelCompletions = await _childLevelCompletionRepository.GetAll();
        
        // Return the found completions
        return Ok(levelCompletions);
    }

    // Get a specific level completion
    [HttpGet("{levelId}")]
    public async Task<IActionResult> GetLevelCompletion(int levelId)
    {
        // Query for completion in DB
        ChildLevelCompletion? levelCompletion = await _childLevelCompletionRepository.GetByLevelId(levelId);

        // Check if ceompletion is found
        if (levelCompletion == null)
            return NotFound();

        // return completion
        return Ok(levelCompletion);
    }

    // Record a new level completion
    [HttpPost("{levelId}/{stickerId}")]
    public async Task<IActionResult> RecordLevelCompletion(int levelId, int stickerId)
    {
        // Executing RecordingCompletionQuery with error handling
        try{ await _childLevelCompletionRepository.RecordCompletion(levelId, stickerId); }
        catch (Exception ex){ return StatusCode(500, $"Something went wrong while recording completion: {ex}"); }
        
        // Return status Ok if Recording Completion went good
        return Ok();
    }

    // Delete a completion record
    [HttpDelete("{levelId}")]
    public async Task<IActionResult> DeleteLevelCompletion(int levelId)
    {
        // Executing DeletingCompletion with error handling
        try { await _childLevelCompletionRepository.DeleteCompletion(levelId); }
        catch (Exception ex) { return StatusCode(500, $"Something went wrong while deleting completion: {ex}"); }

        // Return status NoContent if Recording Completion went good
        return NoContent();
    }
}
