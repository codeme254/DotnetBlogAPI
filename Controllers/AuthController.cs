using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Models;
using IdGen;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[Route("api/[controller]")]
public class AuthController(AppDbContext dbContext, IdGenerator idGen) : ControllerBase
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IdGenerator _idGen = idGen;

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
    {
        string PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password);
        var user = new User
        {
            Id = _idGen.CreateId(),
            Username = registerUserDTO.Username,
            Email = registerUserDTO.Email,
            PasswordHash = PasswordHash
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(null, new
        {
            Status = StatusCodes.Status201Created,
            Message = "User created successfully"
        });
    }
}