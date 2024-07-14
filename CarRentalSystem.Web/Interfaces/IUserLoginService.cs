using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Interfaces;

public interface IUserLoginService
{
    Task<SignInResult> LoginUserAsync(LoginViewModel model);
    Task LogoutUserAsync();
}