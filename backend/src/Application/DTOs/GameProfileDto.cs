namespace VelocitySocial.Application.DTOs;

public class GameProfileDto
{
    public Guid Id { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string GamerTag { get; set; } = string.Empty;
}
