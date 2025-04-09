using VelocitySocial.Core.Entities;
using VelocitySocial.Core.Interfaces;
using VelocitySocial.Infrastructure.Data;

namespace VelocitySocial.Application.Services;

public class GameProfileService : IGameProfileService
{
    private readonly VelocityDbContext _context;

    public GameProfileService(VelocityDbContext context)
    {
        _context = context;
    }

    public async Task<GameProfile> AddGameProfileAsync(Guid userId, string platform, string gamerTag)
    {
        var profile = new GameProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Platform = platform,
            GamerTag = gamerTag
        };
        _context.GameProfiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }
}
