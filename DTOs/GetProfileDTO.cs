namespace BlogAPI.DTOs;

public class GetProfileDTO
{
    public long UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public DateTime DateJoined { get; set; }
    public DateTime LastUpdated { get; set; }
}