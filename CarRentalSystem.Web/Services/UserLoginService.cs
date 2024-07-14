using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Models;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Services;

public class UserLoginService : IUserLoginService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly INotificationService _notificationService;

    public UserLoginService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        INotificationService notificationService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _notificationService = notificationService;
    }

    public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            _notificationService.AddNotification(
                "Please confirm your email address before logging in.",
                NotificationType.Warning);
            return SignInResult.Failed;
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _notificationService.AddNotification(
                "Login successful!",
                NotificationType.Success);
        }

        return result;
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
        _notificationService.AddNotification("You've been logged out", NotificationType.Info);
    }
}