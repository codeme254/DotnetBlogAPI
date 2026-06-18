using BlogAPI.Models;

namespace BlogAPI.Services;

public interface IJwtTokenService
{
    string CreateToken(User user);
}