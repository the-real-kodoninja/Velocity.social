using Microsoft.EntityFrameworkCore;
using VelocitySocial.Application.Services;
using VelocitySocial.Core.Interfaces;
using VelocitySocial.Infrastructure.Data;
using System.Text.Json.Serialization; // Add this

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // Handle cycles
        options.JsonSerializerOptions.WriteIndented = true; // Optional: prettier JSON
    });
builder.Services.AddDbContext<VelocityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infrastructure")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameProfileService, GameProfileService>();

var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
