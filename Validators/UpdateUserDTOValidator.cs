using BlogAPI.DTOs;
using BlogAPI.Repositories;
using FluentValidation;

namespace BlogAPI.Validators;

public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserDTOValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(col => col.Username)
        .MustAsync(BeUniqueUsernameAsync).WithMessage("Username is already taken")
        .Length(3, 20).WithMessage("Length of username should be between 3 and 20 characters");

        RuleFor(col => col.Email)
        .MustAsync(BeUniqueEmailAsync).WithMessage("Email is already taken")
        .EmailAddress().WithMessage("Invalid email address format");
    }

    private async Task<bool> BeUniqueEmailAsync(string email, CancellationToken cancellationToken)
    {
        return !await _userRepository.EmailExistsAsync(email);
    }

    private async Task<bool> BeUniqueUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return !await _userRepository.UsernameExistsAsync(username);
    }
}