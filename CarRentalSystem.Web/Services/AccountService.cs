using AutoMapper;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Models;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public AccountService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        INotificationService notificationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
    {
        var user = _mapper.Map<ApplicationUser>(model);

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return result;
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        _notificationService.AddNotification(
            "You've been successfully registered! Log into your account to continue.",
            NotificationType.Success);

        return result;
    }

    public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
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