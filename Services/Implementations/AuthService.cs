using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Exceptions;
using BlogAPI.Mappers;
using BlogAPI.Models;
using BlogAPI.Repositories;
using IdGen;

namespace BlogAPI.Services.Implementations;

public class AuthService(
    IUserRepository userRepository,
    IdGenerator idGen,
    IJwtTokenService jwtTokenService,
    UserMapper userMapper
) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IdGenerator _idGen = idGen;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly UserMapper _userMapper = userMapper;

    public async Task<string> LoginAsync(LoginDTO loginDTO)
    {
        var user = await _userRepository.GetUserAsync(loginDTO.Identifier)
        ?? throw new InvalidLoginCredentialsException("Invalid login credentials");

        bool passwordsMatch = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash);

        if (passwordsMatch == false) throw new InvalidLoginCredentialsException("Invalid login credentials (p)");

        var token = _jwtTokenService.CreateJwtToken(user);
        return token;
    }

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

    public async Task<GetProfileDTO?> GetProfileAsync(long userId)
    {
        var user = await _userRepository.GetUserProfileAsync(userId)
        ?? throw new NotFoundException("User not found");

        return _userMapper.UserToGetProfileDTO(user);
    }
}