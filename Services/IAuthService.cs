using BlogAPI.DTOs;

namespace BlogAPI.Services;

public interface IAuthService
{
    Task RegisterUserAsync(RegisterUserDTO registerUserDTO);
    Task<string> LoginAsync(LoginDTO loginDTO);
    Task<GetProfileDTO?> GetProfileAsync(long userId);
}