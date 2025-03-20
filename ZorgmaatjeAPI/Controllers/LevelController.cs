using Microsoft.AspNetCore.Mvc;
using ZorgmaatjeAPI.Repositories;
using ZorgmaatjeAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace ZorgmaatjeAPI.Controllers;

[Route("api/levels")]
[ApiController]
public class LevelController : ControllerBase
{
    private readonly LevelRepository _levelRepository;

    public LevelController(LevelRepository levelRepository)
    {
        _levelRepository = levelRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllStickers()
    {
        IEnumerable<Level> levels = await _levelRepository.GetAll();

        return Ok(levels);
    }

    [HttpGet("{levelId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStickerById(int levelId)
    {
        Level? level = await _levelRepository.GetById(levelId);

        if (level == null)
            return StatusCode(404, $"No level where 'id = {levelId}' is found");

        return Ok(level);
    }
}
