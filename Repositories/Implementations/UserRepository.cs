using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories.Implementations;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Email == email, cancellationToken) != null;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Username == username, cancellationToken) != null;
    }
}