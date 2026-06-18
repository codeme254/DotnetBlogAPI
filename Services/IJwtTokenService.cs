using BlogAPI.Models;

namespace BlogAPI.Services;

public interface IJwtTokenService
{
    string CreateJwtToken(User user);
}