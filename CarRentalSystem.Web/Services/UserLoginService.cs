using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Models;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Services;

public class UserLoginService : IUserLoginService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;

    public UserLoginService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IMediator mediator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mediator = mediator;
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
            await _mediator.Publish(new UserNotification
            {
                Message = "Please confirm your email address before logging in.",
                Type = UserNotificationType.Warning
            });
            return SignInResult.Failed;
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            await _mediator.Publish(new UserNotification
            {
                Message = "Login successful!",
                Type = UserNotificationType.Success
            });
        }

        return result;
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
        await _mediator.Publish(new UserNotification
        {
            Message = "You've been logged out",
            Type = UserNotificationType.Info
        });
    }
}