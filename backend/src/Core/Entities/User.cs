namespace VelocitySocial.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<GameProfile> GameProfiles { get; set; } = new();
    public List<Friend> Friends { get; set; } = new(); // Friends where this user is the requester
    public List<Friend> FriendOf { get; set; } = new(); // Friends where this user is the target
    public List<Post> Posts { get; set; } = new();
}
