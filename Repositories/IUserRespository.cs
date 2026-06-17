using BlogAPI.Models;

namespace BlogAPI.Repositories;

public interface IUserRepository
{
    void Add(User user);
    Task SaveChangesAsync();
    Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
}