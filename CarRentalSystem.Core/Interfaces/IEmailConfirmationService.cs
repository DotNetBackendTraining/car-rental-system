using CarRentalSystem.Core.Entities;

namespace CarRentalSystem.Core.Interfaces;

public interface IEmailConfirmationService
{
    Task SendConfirmationEmailAsync(ApplicationUser user);
    Task SendPasswordResetEmailAsync(ApplicationUser user);
}