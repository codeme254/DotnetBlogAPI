using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasIndex(col => col.Email)
            .IsUnique()
            .HasDatabaseName("IX_Users_Email");

            entity.HasIndex(col => col.Username)
            .IsUnique()
            .HasDatabaseName("IX_Users_Username");
        });
    }
}