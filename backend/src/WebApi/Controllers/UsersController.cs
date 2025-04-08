using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VelocitySocial.Infrastructure.Data;

namespace VelocitySocial.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly VelocityDbContext _context;

    public UsersController(VelocityDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.Include(u => u.GameProfiles).ToListAsync();
        return Ok(users);
    }
}
