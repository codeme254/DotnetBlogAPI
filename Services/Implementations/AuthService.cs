using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Models;
using BlogAPI.Repositories;
using IdGen;

namespace BlogAPI.Services.Implementations;

public class AuthService(IUserRepository userRepository, IdGenerator idGen) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
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

        _userRepository.Add(user);
        await _userRepository.SaveChangesAsync();
    }
}