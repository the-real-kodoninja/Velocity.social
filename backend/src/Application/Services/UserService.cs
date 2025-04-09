using VelocitySocial.Core.Entities;
using VelocitySocial.Core.Interfaces;
using VelocitySocial.Infrastructure.Data;

namespace VelocitySocial.Application.Services;

public class UserService : IUserService
{
    private readonly VelocityDbContext _context;

    public UserService(VelocityDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(string username, string email)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            CreatedAt = DateTime.UtcNow
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task AddFriendAsync(Guid userId, Guid friendId)
    {
        var friendship = new Friend
        {
            UserId = userId,
            FriendId = friendId,
            ConnectedAt = DateTime.UtcNow
        };
        _context.Friends.Add(friendship);
        await _context.SaveChangesAsync();
    }
}
