using System.Security.Claims;
using BlogAPI.DTOs;
using BlogAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController(
    IAuthService authService,
    IValidator<RegisterUserDTO> validator,
    IValidator<UpdateUserDTO> updateUserValidator
) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<RegisterUserDTO> _validator = validator;
    private readonly IValidator<UpdateUserDTO> _updateUserValidator = updateUserValidator;

    [HttpPost("register")]
    [ApiVersion("1.0")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
    {
        var validationResults = await _validator.ValidateAsync(registerUserDTO);

        if (!validationResults.IsValid)
        {
            var errors = validationResults.Errors.Select(e => e.ErrorMessage);

            return BadRequest(new
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "User registration failed",
                Errors = errors
            });
        }
        // var apiVersion = HttpContext.GetRequestedApiVersion(); // Getting the api version
        await _authService.RegisterUserAsync(registerUserDTO);

        return StatusCode(StatusCodes.Status201Created, new
        {
            Status = 201,
            Message = "User created successfully"
        });
    }

    [HttpPost("login")]
    [ApiVersion("1.0")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var token = await _authService.LoginAsync(loginDTO);
        return Ok(new
        {
            Message = "User logged in successfully",
            Token = token
        });
    }

    [HttpGet("profile")]
    [ApiVersion("1.0")]
    [Authorize]
    public async Task<ActionResult> GetProfile()
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var profile = await _authService.GetProfileAsync(long.Parse(sub));
        return Ok(profile);
    }

    [HttpPatch("profile")]
    [ApiVersion("1.0")]
    [Authorize]
    public async Task<ActionResult> UpdateProfile([FromBody] UpdateUserDTO updateUserDTO)
    {
        var validationResults = await _updateUserValidator.ValidateAsync(updateUserDTO);

        if (!validationResults.IsValid)
        {
            var errors = validationResults.Errors.Select(e => e.ErrorMessage);

            return BadRequest(new
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "Profile update failed",
                Errors = errors
            });
        }

        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        var updatedUser = await _authService.UpdateUserAsync(long.Parse(sub), updateUserDTO);
        return Ok(updatedUser);
    }
}