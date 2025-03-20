using Microsoft.AspNetCore.Mvc;
using ZorgmaatjeAPI.Repositories;
using ZorgmaatjeAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace ZorgmaatjeAPI.Controllers;

[Route("api/stickers")]
[ApiController]
public class StickerController : ControllerBase
{
    private readonly StickerRepository _stickerRepository;

    public StickerController(StickerRepository stickerRepository)
    {
        _stickerRepository = stickerRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllStickers()
    {
        IEnumerable<Sticker> stickers = await _stickerRepository.GetAll();

        return Ok(stickers);
    }

    [HttpGet("{stickerId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStickerById(int stickerId)
    {
        Sticker? sticker = await _stickerRepository.GetById(stickerId);

        if (sticker == null)
            return StatusCode(404, $"No sticker where 'id = {stickerId}' is found");

        return Ok(sticker);
    }
}
