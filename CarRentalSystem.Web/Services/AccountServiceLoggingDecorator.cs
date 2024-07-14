using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Services;

public class AccountServiceLoggingDecorator : IAccountService
{
    private readonly IAccountService _service;
    private readonly ILogger<AccountServiceLoggingDecorator> _logger;

    public AccountServiceLoggingDecorator(
        IAccountService service,
        ILogger<AccountServiceLoggingDecorator> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<ProfileViewModel> GetCurrentUserProfileAsync()
    {
        _logger.LogInformation("Getting current user profile.");
        var result = await _service.GetCurrentUserProfileAsync();
        _logger.LogInformation("Current user profile retrieved successfully.");
        return result;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
    {
        _logger.LogInformation("Registering new user with email: {Email}", model.Email);
        var result = await _service.RegisterUserAsync(model);
        if (!result.Succeeded)
        {
            _logger.LogWarning("User registration failed for email: {Email}", model.Email);
        }
        else
        {
            _logger.LogInformation("User registered successfully with email: {Email}", model.Email);
        }

        return result;
    }

    public async Task<IdentityResult> UpdateCurrentUserProfileAsync(ProfileViewModel model)
    {
        _logger.LogInformation("Updating profile for current user.");
        var result = await _service.UpdateCurrentUserProfileAsync(model);
        if (result.Succeeded)
        {
            _logger.LogInformation("Profile updated successfully.");
        }
        else
        {
            _logger.LogWarning("Profile update failed.");
        }

        return result;
    }

    public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
    {
        _logger.LogInformation("Logging in user with email: {Email}", model.Email);
        var result = await _service.LoginUserAsync(model);
        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in successfully with email: {Email}", model.Email);
        }
        else
        {
            _logger.LogWarning("Login failed for email: {Email}", model.Email);
        }

        return result;
    }

    public async Task LogoutUserAsync()
    {
        _logger.LogInformation("Logging out user.");
        await _service.LogoutUserAsync();
        _logger.LogInformation("User logged out successfully.");
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
    {
        _logger.LogInformation("Confirming email for user ID: {UserId}", userId);
        var result = await _service.ConfirmEmailAsync(userId, token);
        if (result.Succeeded)
        {
            _logger.LogInformation("Email confirmed successfully for user ID: {UserId}", userId);
        }
        else
        {
            _logger.LogWarning("Email confirmation failed for user ID: {UserId}", userId);
        }

        return result;
    }
}