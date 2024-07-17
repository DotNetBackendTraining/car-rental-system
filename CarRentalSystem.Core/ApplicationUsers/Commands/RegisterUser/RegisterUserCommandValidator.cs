using CarRentalSystem.Core.Interfaces;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.RegisterUser;

public class RegisterUserCommandValidator : UserProfileAbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(ICountryService countryService) : base(countryService)
    {
    }
}