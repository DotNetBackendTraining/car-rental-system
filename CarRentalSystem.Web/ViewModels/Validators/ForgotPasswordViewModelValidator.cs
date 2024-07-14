using FluentValidation;

namespace CarRentalSystem.Web.ViewModels.Validators;

public class ForgotPasswordViewModelValidator : AbstractValidator<ForgotPasswordViewModel>
{
    public ForgotPasswordViewModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}