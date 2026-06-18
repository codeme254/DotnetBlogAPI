using BlogAPI.Models;

namespace BlogAPI.Services.Implementations;

public class JwtTokenService : IJwtTokenService
{
    public string CreateToken(User user)
    {
        return "Token";
    }
}