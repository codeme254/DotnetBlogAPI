using BlogAPI.DTOs;

namespace BlogAPI.Services;

public interface IAuthService
{
    Task RegisterUserAsync(RegisterUserDTO registerUserDTO);
    Task<string> LoginAsync(LoginDTO loginDTO, CancellationToken cancellationToken);
    Task<GetProfileDTO?> GetProfileAsync(long userId, CancellationToken cancellationToken);
}