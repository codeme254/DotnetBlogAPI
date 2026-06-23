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

    public async Task<User?> GetUserAsync(string identifier)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(
            u => u.Username == identifier ||
            u.Email == identifier);
    }

    public async Task<User?> GetUserProfileAsync(long id)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Username == username) != null;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Email == email) != null;
    }

    public void UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}