using BlogAPI.DTOs;

namespace BlogAPI.Services;

public interface IAuthService
{
    Task RegisterUserAsync(RegisterUserDTO registerUserDTO);
}