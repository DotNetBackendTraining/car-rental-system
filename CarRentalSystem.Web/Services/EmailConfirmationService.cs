using System.Text.Encodings.Web;
using CarRentalSystem.Core.Entities;
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

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new InvalidOperationException("HttpContext cannot be null");
        }

        var confirmationLink = _urlHelper.Action(
            action: "ConfirmEmail",
            controller: "Account",
            values: new { userId = user.Id, token },
            protocol: httpContext.Request.Scheme);

        if (string.IsNullOrEmpty(confirmationLink))
        {
            throw new InvalidOperationException("Failed to generate confirmation link");
        }

        var encodedLink = HtmlEncoder.Default.Encode(confirmationLink);
        await _emailSender.SendEmailAsync(user.Email!, "Confirm your email",
            $"Please confirm your email by clicking <a href='{encodedLink}'>here</a>.");
    }
}