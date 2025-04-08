using Microsoft.EntityFrameworkCore;
using VelocitySocial.Core.Entities;

namespace VelocitySocial.Infrastructure.Data;

public class VelocityDbContext : DbContext
{
    public VelocityDbContext(DbContextOptions<VelocityDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<GameProfile> GameProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.GameProfiles)
            .WithOne(gp => gp.User)
            .HasForeignKey(gp => gp.UserId);
    }
}
