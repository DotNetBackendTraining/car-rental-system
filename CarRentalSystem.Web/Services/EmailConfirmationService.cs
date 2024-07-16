using System.Text.Encodings.Web;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Web.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CarRentalSystem.Web.Services;

public class EmailConfirmationService : IEmailConfirmationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUrlHelper _urlHelper;

    public EmailConfirmationService(
        UserManager<ApplicationUser> userManager,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _httpContextAccessor = httpContextAccessor;

        var actionContext = actionContextAccessor.ActionContext;
        if (actionContext == null)
        {
            throw new InvalidOperationException("ActionContext cannot be null");
        }

        _urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
    }

    public async Task SendConfirmationEmailAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await SendEmailAsync(user, token, "Confirm your email",
            "Please confirm your email by clicking <a href='{0}'>here</a>.", "ConfirmEmail");
    }

    public async Task SendPasswordResetEmailAsync(ApplicationUser user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        await SendEmailAsync(user, token, "Reset Password",
            "Please reset your password by clicking <a href='{0}'>here</a>.", "ResetPassword");
    }

    private async Task SendEmailAsync(
        ApplicationUser user,
        string token,
        string subject,
        string messageFormat,
        string actionName)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new InvalidOperationException("HttpContext cannot be null");
        }

        var link = _urlHelper.Action(
            action: actionName,
            controller: "Account",
            values: new { userId = user.Id, token },
            protocol: httpContext.Request.Scheme);

        if (string.IsNullOrEmpty(link))
        {
            throw new InvalidOperationException($"Failed to generate {actionName} link");
        }

        var encodedLink = HtmlEncoder.Default.Encode(link);
        var message = string.Format(messageFormat, encodedLink);
        await _emailSender.SendEmailAsync(user.Email!, subject, message);
    }
}