using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Interfaces;

public interface IAccountService
{
    Task<ProfileViewModel> GetCurrentUserProfileAsync();

    Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);

    Task<IdentityResult> UpdateCurrentUserProfileAsync(ProfileViewModel model);

    Task<SignInResult> LoginUserAsync(LoginViewModel model);

    Task LogoutUserAsync();
}