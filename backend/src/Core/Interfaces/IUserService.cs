using VelocitySocial.Core.Entities;

namespace VelocitySocial.Core.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(string username, string email);
    Task AddFriendAsync(Guid userId, Guid friendId);
}
