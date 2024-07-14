using AutoMapper;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Models;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Services;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly INotificationService _notificationService;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public UserRegistrationService(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        INotificationService notificationService,
        IEmailConfirmationService emailConfirmationService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _notificationService = notificationService;
        _emailConfirmationService = emailConfirmationService;
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

    public async Task<IdentityResult> ConfirmUserEmailAsync(string userId, string token)
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

    public async Task<IdentityResult> ResetUserPasswordAsync(ResetPasswordViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (result.Succeeded)
        {
            _notificationService.AddNotification(
                "Your password has been reset successfully.",
                NotificationType.Success);
        }

        return result;
    }

    public async Task InitiateUserPasswordResetAsync(ForgotPasswordViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            _notificationService.AddNotification(
                "User with the given email address does not exist.",
                NotificationType.Error);
            return;
        }

        await _emailConfirmationService.SendPasswordResetEmailAsync(user);
        _notificationService.AddNotification("Password reset email has been sent.", NotificationType.Info);
    }
}