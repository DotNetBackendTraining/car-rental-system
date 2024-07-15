using AutoMapper;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Models;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Services;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public UserRegistrationService(
        IMapper mapper,
        IMediator mediator,
        UserManager<ApplicationUser> userManager,
        IEmailConfirmationService emailConfirmationService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _userManager = userManager;
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
        await _mediator.Publish(new UserNotification
        {
            Message = "Registration successful! Please check your email to confirm your account.",
            Type = UserNotificationType.Success
        });

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
            await _mediator.Publish(new UserNotification
            {
                Message = "Email confirmed successfully.",
                Type = UserNotificationType.Success
            });
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
            await _mediator.Publish(new UserNotification
            {
                Message = "Your password has been reset successfully.",
                Type = UserNotificationType.Success
            });
        }

        return result;
    }

    public async Task InitiateUserPasswordResetAsync(ForgotPasswordViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            await _mediator.Publish(new UserNotification
            {
                Message = "User with the given email address does not exist.",
                Type = UserNotificationType.Error
            });
            return;
        }

        await _emailConfirmationService.SendPasswordResetEmailAsync(user);
        await _mediator.Publish(new UserNotification
        {
            Message = "Password reset email has been sent.",
            Type = UserNotificationType.Info
        });
    }
}