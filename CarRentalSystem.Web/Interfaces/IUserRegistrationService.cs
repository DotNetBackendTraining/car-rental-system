using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Interfaces;

public interface IUserRegistrationService
{
    Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
    Task<IdentityResult> ConfirmUserEmailAsync(string userId, string token);
    Task<IdentityResult> ResetUserPasswordAsync(ResetPasswordViewModel model);
    Task InitiateUserPasswordResetAsync(ForgotPasswordViewModel model);
}