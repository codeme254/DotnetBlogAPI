using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Models;
using IdGen;

namespace BlogAPI.Services.Implementations;

public class AuthService(AppDbContext dbContext, IdGenerator idGen) : IAuthService
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IdGenerator _idGen = idGen;
    public async Task RegisterUserAsync(RegisterUserDTO registerUserDTO)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password);
        var user = new User
        {
            Id = _idGen.CreateId(),
            Username = registerUserDTO.Username,
            Email = registerUserDTO.Email,
            PasswordHash = passwordHash
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }
}