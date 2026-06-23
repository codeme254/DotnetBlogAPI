using BlogAPI.Models;

namespace BlogAPI.Repositories;

public interface IUserRepository
{
    void Add(User user);
    Task<User?> GetUserAsync(string identifier);
    Task<User?> GetUserProfileAsync(long id);
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
    void UpdateUser(User user);
    Task SaveChangesAsync();
}