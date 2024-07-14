using CarRentalSystem.Core.Interfaces;

namespace CarRentalSystem.Web.ViewModels.Validators;

public class RegisterViewModelValidator : ProfileViewModelValidator<RegisterViewModel>
{
    public RegisterViewModelValidator(ICountryService countryService) : base(countryService)
    {
    }
}