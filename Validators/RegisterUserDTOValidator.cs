using BlogAPI.DTOs;
using BlogAPI.Repositories;
using FluentValidation;

namespace BlogAPI.Validators;

public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
{
    private readonly IUserRepository _userRepository;
    public RegisterUserDTOValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(col => col.Username)
        .NotEmpty().WithMessage("Username is required")
        .MustAsync(BeUniqueUsernameAsync).WithMessage("Username is already taken")
        .Length(3, 20).WithMessage("Length of username should be between 3 and 20 characters");

        RuleFor(col => col.Email)
        .NotEmpty().WithMessage("Email address is required")
        .EmailAddress().WithMessage("Invalid email format")
        .MustAsync(BeUniqueEmailAsync).WithMessage("Email address is already taken");

        RuleFor(col => col.Password)
        .NotEmpty().WithMessage("Password is required")
        .MinimumLength(8).WithMessage("Password should be at least 8 characters long")
        .Must(BeComplexPassword).WithMessage("Password must contain at least 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character");
    }

    private async Task<bool> BeUniqueEmailAsync(string email, CancellationToken cancellationToken)
    {
        return !await _userRepository.EmailExistsAsync(email);
    }

    private async Task<bool> BeUniqueUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return !await _userRepository.UsernameExistsAsync(username);
    }

    private static bool BeComplexPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return false;
        return password.Any(char.IsUpper)
        && password.Any(char.IsLower)
        && password.Any(char.IsDigit)
        && password.Any(ch => !char.IsLetterOrDigit(ch));
    }
}