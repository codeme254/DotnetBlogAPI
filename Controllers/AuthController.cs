using BlogAPI.DTOs;
using BlogAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController(IAuthService authService, IValidator<RegisterUserDTO> validator) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<RegisterUserDTO> _validator = validator;

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

    // [HttpPost("register")]
    // [ApiVersion("2.0")]
    // public async Task<ActionResult> RegisterV2([FromBody] RegisterUserDTO registerUserDTO)
    // {
    //     return Ok(new
    //     {
    //         Message = "User created successfully from v2"
    //     });
    // }
}