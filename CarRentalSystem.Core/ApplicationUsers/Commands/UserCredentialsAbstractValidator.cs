using FluentValidation;

namespace CarRentalSystem.Core.ApplicationUsers.Commands;

public class UserCredentialsAbstractValidator<T> : AbstractValidator<T>
    where T : UserCredentialsCommandBase
{
    public UserCredentialsAbstractValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}