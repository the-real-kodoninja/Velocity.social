using Microsoft.EntityFrameworkCore;
using VelocitySocial.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Adds support for controllers
builder.Services.AddDbContext<VelocityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Configures PostgreSQL database context
builder.Services.AddEndpointsApiExplorer(); // Enables API exploration for minimal APIs
builder.Services.AddSwaggerGen(); // Adds Swagger generation

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger UI in development
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirects HTTP to HTTPS
app.UseAuthorization(); // Adds authorization middleware
app.MapControllers(); // Maps controller routes

// Weather forecast endpoint
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
