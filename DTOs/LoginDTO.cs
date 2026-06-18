namespace BlogAPI.DTOs;

public class LoginDTO
{
    public string Identifier { get; set; } = null!;
    public string Password { get; set; } = null!;
}