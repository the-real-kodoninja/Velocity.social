using Microsoft.EntityFrameworkCore;
using VelocitySocial.Core.Entities;

namespace VelocitySocial.Infrastructure.Data;

public class VelocityDbContext : DbContext
{
    public VelocityDbContext(DbContextOptions<VelocityDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<GameProfile> GameProfiles { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.GameProfiles)
            .WithOne(gp => gp.User)
            .HasForeignKey(gp => gp.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Friends)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.FriendOf)
            .WithOne(f => f.FriendUser)
            .HasForeignKey(f => f.FriendId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Friend>()
            .HasKey(f => new { f.UserId, f.FriendId });
    }
}
