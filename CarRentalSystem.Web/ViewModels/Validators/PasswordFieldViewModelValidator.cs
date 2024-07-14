using FluentValidation;

namespace CarRentalSystem.Web.ViewModels.Validators;

public class PasswordFieldViewModelValidator<T> : AbstractValidator<T> where T : PasswordFieldViewModel
{
    protected PasswordFieldViewModelValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}