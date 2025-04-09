using VelocitySocial.Core.Entities;

namespace VelocitySocial.Core.Interfaces;

public interface IGameProfileService
{
    Task<GameProfile> AddGameProfileAsync(Guid userId, string platform, string gamerTag);
}
