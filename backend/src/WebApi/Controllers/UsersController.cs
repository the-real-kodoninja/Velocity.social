using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VelocitySocial.Application.DTOs;
using VelocitySocial.Core.Entities;
using VelocitySocial.Core.Interfaces;
using VelocitySocial.Infrastructure.Data;

namespace VelocitySocial.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly VelocityDbContext _context;
    private readonly IUserService _userService;

    public UsersController(VelocityDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.Include(u => u.GameProfiles).ToListAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        var user = await _userService.CreateUserAsync(userDto.Username, userDto.Email);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpPost("{userId}/friends/{friendId}")]
    public async Task<IActionResult> AddFriend(Guid userId, Guid friendId)
    {
        await _userService.AddFriendAsync(userId, friendId);
        return NoContent();
    }

    [HttpPost("{userId}/gameprofiles")]
    public async Task<IActionResult> AddGameProfile(Guid userId, [FromBody] GameProfileDto gameProfileDto)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound();

        var gameProfile = new GameProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Platform = gameProfileDto.Platform,
            GamerTag = gameProfileDto.GamerTag
        };

        _context.GameProfiles.Add(gameProfile);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new { id = userId }, gameProfile);
    }
}
