using BlogAPI.Data;
using BlogAPI.Models;

namespace BlogAPI.Repositories.Implementations;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}