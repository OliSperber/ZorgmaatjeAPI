using Microsoft.AspNetCore.Mvc;
using ZorgmaatjeAPI.Services;
using ZorgmaatjeAPI.Repositories;
using ZorgmaatjeAPI.Models;
using System.Threading.Tasks;

namespace ZorgmaatjeAPI.Controllers;

[Route("api/child")]
[ApiController]
public class ChildrenController : ControllerBase
{
    private readonly ChildRepository _childRepository;
    private readonly AuthorizationService _authorizationService;

    // Constructor for dependency injection
    public ChildrenController(ChildRepository childRepository, AuthorizationService authorizationService)
    {
        _childRepository = childRepository;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetChild()
    {
        // Obtaining child from DB
        Child? child = await _childRepository.GetChild();

        // Throw error if no child is found
        if (child == null)
            return StatusCode(500, "No child has been found for the user");

        // Return child if found
        return Ok(child);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChild([FromBody] Child child)
    {
        // Checks if ModelState is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Creating diary entry with error handling
        try
        {
            var createdChild = await _childRepository.CreateAsync(child);
            return Created(string.Empty, createdChild);
        }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex}"); }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateChild([FromBody] Child child)
    {
        // Updating child with error handling
        try { return Ok(await _childRepository.UpdateAsync(child)); }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex}"); }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteChild()
    {
        // Deleting child with error handling
        try { await _childRepository.DeleteAsync(); }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex}"); }

        // Returning NoContent status if child is deleted succesfully
        return NoContent();
    }
}
