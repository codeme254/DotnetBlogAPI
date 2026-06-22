using BlogAPI.Models;

namespace BlogAPI.Repositories;

public interface IUserRepository
{
    void Add(User user);
    Task<User?> GetUserAsync(string identifier, CancellationToken cancellationToken);
    Task<User?> GetUserProfileAsync(long id, CancellationToken cancellationToken);
    Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
    Task SaveChangesAsync();
}