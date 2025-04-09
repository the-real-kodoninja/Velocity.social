namespace VelocitySocial.Core.Entities;

public class Friend
{
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
    public DateTime ConnectedAt { get; set; }
    public User User { get; set; } = null!;
    public User FriendUser { get; set; } = null!;
}
