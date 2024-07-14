using AutoMapper;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Models;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly INotificationService _notificationService;
    private readonly IUserAccessorService _userAccessorService;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public UserProfileService(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        INotificationService notificationService,
        IUserAccessorService userAccessorService,
        IEmailConfirmationService emailConfirmationService)
    {
        _userManager = userManager;
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
}