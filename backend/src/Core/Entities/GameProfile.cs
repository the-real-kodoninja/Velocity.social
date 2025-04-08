namespace VelocitySocial.Core.Entities;

public class GameProfile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Platform { get; set; } = string.Empty; // e.g., Xbox, Steam, Kodoverse
    public string GamerTag { get; set; } = string.Empty;
    public User User { get; set; } = null!;
}
