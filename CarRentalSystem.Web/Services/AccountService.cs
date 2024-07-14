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
    private readonly IUserAccessorService _userAccessorService;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public AccountService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        INotificationService notificationService,
        IUserAccessorService userAccessorService,
        IEmailConfirmationService emailConfirmationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _notificationService = notificationService;
        _userAccessorService = userAccessorService;
        _emailConfirmationService = emailConfirmationService;
    }

    public async Task<ProfileViewModel> GetCurrentUserProfileAsync()
    {
        var user = await _userAccessorService.GetCurrentUserAsync();
        return _mapper.Map<ProfileViewModel>(user);
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
    {
        var user = _mapper.Map<ApplicationUser>(model);

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return result;
        }

        await _emailConfirmationService.SendConfirmationEmailAsync(user);

        _notificationService.AddNotification(
            "Registration successful! Please check your email to confirm your account.",
            NotificationType.Success);

        return result;
    }

    public async Task<IdentityResult> UpdateCurrentUserProfileAsync(ProfileViewModel model)
    {
        var user = await _userAccessorService.GetCurrentUserAsync();
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        var emailChanged = !string.Equals(user.Email, model.Email, StringComparison.OrdinalIgnoreCase);
        _mapper.Map(model, user);

        if (emailChanged)
        {
            user.EmailConfirmed = false;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return result;
        }

        _notificationService.AddNotification("Profile updated successfully.", NotificationType.Success);
        if (!emailChanged)
        {
            return result;
        }

        await _emailConfirmationService.SendConfirmationEmailAsync(user);
        _notificationService.AddNotification(
            "Please check your email to confirm your account.",
            NotificationType.Warning);

        return result;
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

    public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            _notificationService.AddNotification("Email confirmed successfully.", NotificationType.Success);
        }

        return result;
    }
}