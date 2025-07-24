using EdTech.Quiz.Application.DTOs.Request;
using FluentValidation;

namespace EdTech.Quiz.Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDTO>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotNull().WithMessage("Username cannot be null.")
            .NotEmpty().WithMessage("Username cannot be empty.")
            .Matches(@"^[a-zA-Z0-9_-]{3,20}$").WithMessage("Username must be 3-20 characters long and can only contain letters, numbers, underscores, or hyphens.");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .Matches(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.[a-zA-Z]{2,}([-.]\w+)*$").WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password cannot be empty.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

    }
}
