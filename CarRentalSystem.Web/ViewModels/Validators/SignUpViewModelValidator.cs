using CarRentalSystem.Web.Interfaces;
using FluentValidation;

namespace CarRentalSystem.Web.ViewModels.Validators;

public class SignUpViewModelValidator : UserBaseViewModelValidator<SignUpViewModel>
{
    private readonly ICountryService _countryService;

    public SignUpViewModelValidator(ICountryService countryService)
    {
        _countryService = countryService;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .MaximumLength(50).WithMessage("First Name cannot be longer than 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required.")
            .MaximumLength(50).WithMessage("Last Name cannot be longer than 50 characters.");

        RuleFor(x => x.Password)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Password and Confirm Password do not match.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches(@"^\d{10}$").WithMessage("Phone Number must be 10 digits.");

        RuleFor(x => x.AddressLine1)
            .NotEmpty().WithMessage("Address Line 1 is required.")
            .MaximumLength(100).WithMessage("Address Line 1 cannot be longer than 100 characters.");

        RuleFor(x => x.AddressLine2)
            .MaximumLength(100).WithMessage("Address Line 2 cannot be longer than 100 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City cannot be longer than 50 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(50).WithMessage("Country cannot be longer than 50 characters.")
            .Must(BeAValidCountry).WithMessage("Invalid country selected.");

        RuleFor(x => x.DriversLicenseNumber)
            .NotEmpty().WithMessage("Driver's License Number is required.")
            .MaximumLength(20).WithMessage("Driver's License Number cannot be longer than 20 characters.");
    }

    private bool BeAValidCountry(string country)
    {
        return _countryService.IsValidCountry(country);
    }
}