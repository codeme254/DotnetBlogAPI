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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
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
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    // Specify the default api version
    options.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client doesn't specify the version, use the default version
    options.AssumeDefaultVersionWhenUnspecified = true;
    // Include api version in response headers
    options.ReportApiVersions = true;
    // Read API version info from the URL segment
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapGet("/", () => "Welcome to the API");
app.MapControllers();

app.Run();
