using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Web.Filters;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRentalSystem.Web.Controllers;

public class AccountController : Controller
{
    private readonly ICountryService _countryService;
    private readonly IUserLoginService _userLoginService;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserRegistrationService _userRegistrationService;

    public AccountController(
        ICountryService countryService,
        IUserLoginService userLoginService,
        IUserProfileService userProfileService,
        IUserRegistrationService userRegistrationService)
    {
        _countryService = countryService;
        _userLoginService = userLoginService;
        _userProfileService = userProfileService;
        _userRegistrationService = userRegistrationService;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        ViewData["Countries"] = _countryService.GetAllCountries();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ServiceFilter(typeof(ValidationFilter<RegisterViewModel>))]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _userRegistrationService.RegisterUserAsync(model);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ServiceFilter(typeof(ValidationFilter<LoginViewModel>))]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _userLoginService.LoginUserAsync(model);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Find");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _userLoginService.LogoutUserAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> UpdateProfile()
    {
        var userProfile = await _userProfileService.GetCurrentUserProfileAsync();
        return View(userProfile);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    [ServiceFilter(typeof(ValidationFilter<ProfileViewModel>))]
    public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _userProfileService.UpdateCurrentUserProfileAsync(model);
        if (result.Succeeded)
        {
            return RedirectToAction("UpdateProfile", "Account");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index", "Home");
        }

        var result = await _userRegistrationService.ConfirmUserEmailAsync(userId, token);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        return View("Error");
    }
}