using CarRentalSystem.Core.Entities;

namespace CarRentalSystem.Web.Interfaces;

public interface IEmailConfirmationService
{
    Task SendConfirmationEmailAsync(ApplicationUser user);
}