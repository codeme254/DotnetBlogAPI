using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Middlewares;
using BlogAPI.Repositories;
using BlogAPI.Repositories.Implementations;
using BlogAPI.Services;
using BlogAPI.Services.Implementations;
using BlogAPI.Validators;
using FluentValidation;
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

// Validators
builder.Services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapGet("/", () => "Welcome to the API");
app.MapControllers();

app.Run();
