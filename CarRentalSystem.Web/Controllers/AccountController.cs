using AutoMapper;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Models;
using CarRentalSystem.Web.Filters;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRentalSystem.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly ICountryService _countryService;
    private readonly INotificationService _notificationService;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        ICountryService countryService,
        INotificationService notificationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _countryService = countryService;
        _notificationService = notificationService;
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

        var user = _mapper.Map<ApplicationUser>(model);

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);

            _notificationService.AddNotification(
                "You've been successfully registered! Log into your account to continue.",
                NotificationType.Success);

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

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _notificationService.AddNotification(
                "Login successful. Feel free to search for your right car!",
                NotificationType.Success);
            return RedirectToAction("Index", "Find");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _notificationService.AddNotification("You've been logged out", NotificationType.Info);
        return RedirectToAction("Index", "Home");
    }
}