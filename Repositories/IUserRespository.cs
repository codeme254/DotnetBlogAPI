using BlogAPI.Models;

namespace BlogAPI.Repositories;

public interface IUserRepository
{
    void Add(User user);
    Task SaveChangesAsync();
}