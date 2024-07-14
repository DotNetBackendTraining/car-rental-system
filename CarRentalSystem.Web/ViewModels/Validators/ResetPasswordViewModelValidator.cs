using FluentValidation;

namespace CarRentalSystem.Web.ViewModels.Validators;

public class ResetPasswordViewModelValidator : AbstractValidator<ResetPasswordViewModel>
{
    public ResetPasswordViewModelValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .StrongPassword();
    }
}