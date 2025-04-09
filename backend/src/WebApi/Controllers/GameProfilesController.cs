using Microsoft.AspNetCore.Mvc;
using VelocitySocial.Application.DTOs;
using VelocitySocial.Core.Entities;
using VelocitySocial.Core.Interfaces;

namespace VelocitySocial.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameProfilesController : ControllerBase
{
    private readonly IGameProfileService _gameProfileService;

    public GameProfilesController(IGameProfileService gameProfileService)
    {
        _gameProfileService = gameProfileService;
    }

    [HttpPost]
    public async Task<IActionResult> AddGameProfile([FromBody] GameProfileDto profileDto)
    {
        var profile = await _gameProfileService.AddGameProfileAsync(
            profileDto.Id, profileDto.Platform, profileDto.GamerTag);
        return CreatedAtAction(nameof(AddGameProfile), new { id = profile.Id }, profile);
    }
}
