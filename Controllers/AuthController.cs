using BlogAPI.Data;
using BlogAPI.DTOs;
using BlogAPI.Models;
using BlogAPI.Services;
using IdGen;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
    {
        await _authService.RegisterUserAsync(registerUserDTO);

        return CreatedAtAction(null, new
        {
            Status = StatusCodes.Status201Created,
            Message = "User created successfully"
        });
    }
}