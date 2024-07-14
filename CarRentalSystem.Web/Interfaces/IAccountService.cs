using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);

    Task<SignInResult> LoginUserAsync(LoginViewModel model);

    Task LogoutUserAsync();
}