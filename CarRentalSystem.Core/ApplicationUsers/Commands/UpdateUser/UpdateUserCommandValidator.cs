using CarRentalSystem.Core.Interfaces;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.UpdateUser;

public class UpdateUserCommandValidator : UserProfileAbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(ICountryService countryService) : base(countryService)
    {
    }
}