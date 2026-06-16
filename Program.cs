using BlogAPI.Data;
using BlogAPI.Repositories;
using BlogAPI.Repositories.Implementations;
using BlogAPI.Services;
using BlogAPI.Services.Implementations;
using IdGen;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// IdGen Service for Snowflake IDs
builder.Services.AddSingleton(_ => new IdGenerator(0));

builder.Services.AddScoped<IAuthService, AuthService>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/", () => "Welcome to the API");
app.MapControllers();

app.Run();
