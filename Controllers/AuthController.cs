using BlogAPI.DTOs;
using BlogAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[Route("api/[controller]")]
public class AuthController(IAuthService authService, IValidator<RegisterUserDTO> validator) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<RegisterUserDTO> _validator = validator;

    [HttpPost("register")]
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
        await _authService.RegisterUserAsync(registerUserDTO);

        return CreatedAtAction(null, new
        {
            Status = StatusCodes.Status201Created,
            Message = "User created successfully"
        });
    }
}