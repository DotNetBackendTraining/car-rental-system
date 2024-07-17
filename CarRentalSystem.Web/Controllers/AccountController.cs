using AutoMapper;
using CarRentalSystem.Core.ApplicationUsers.Commands.ConfirmEmail;
using CarRentalSystem.Core.ApplicationUsers.Commands.ForgotPassword;
using CarRentalSystem.Core.ApplicationUsers.Commands.LoginUser;
using CarRentalSystem.Core.ApplicationUsers.Commands.LogoutUserCommand;
using CarRentalSystem.Core.ApplicationUsers.Commands.RegisterUser;
using CarRentalSystem.Core.ApplicationUsers.Commands.ResetPassword;
using CarRentalSystem.Core.ApplicationUsers.Commands.UpdateUser;
using CarRentalSystem.Core.ApplicationUsers.Queries.CurrentUserQuery;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRentalSystem.Web.Controllers;

public class AccountController : AbstractController
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly ICountryService _countryService;

    public AccountController(
        IMapper mapper,
        ISender sender,
        ICountryService countryService)
    {
        _mapper = mapper;
        _sender = sender;
        _countryService = countryService;
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
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var registerUserCommand = _mapper.Map<RegisterUserCommand>(model);
        var result = await _sender.Send(registerUserCommand);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        HandleErrors(result);
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var loginUserCommand = _mapper.Map<LoginUserCommand>(model);
        var result = await _sender.Send(loginUserCommand);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index", "Find");
        }

        HandleErrors(result);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _sender.Send(new LogoutUserCommand());
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> UpdateProfile()
    {
        var result = await _sender.Send(new CurrentUserQuery());
        if (result.IsFailure)
        {
            HandleErrors(result);
            return View();
        }

        var profileViewModel = _mapper.Map<ProfileViewModel>(result.Value);
        return View(profileViewModel);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
    {
        var updateUserProfileCommand = _mapper.Map<UpdateUserCommand>(model);
        var result = await _sender.Send(updateUserProfileCommand);
        if (result.IsSuccess)
        {
            return RedirectToAction("UpdateProfile", "Account");
        }

        HandleErrors(result);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index", "Home");
        }

        var confirmEmailCommand = new ConfirmEmailCommand
        {
            UserId = userId,
            Token = token
        };

        var result = await _sender.Send(confirmEmailCommand);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        HandleErrors(result);
        return View("Error");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        var resetPasswordCommand = _mapper.Map<ForgotPasswordCommand>(model);
        var result = await _sender.Send(resetPasswordCommand);
        if (result.IsSuccess)
        {
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        HandleErrors(result);
        return View("Error");
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            return BadRequest("A user ID and token must be supplied for password reset.");
        }

        var model = new ResetPasswordViewModel
        {
            UserId = userId,
            Token = token
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        var resetPasswordCommand = _mapper.Map<ResetPasswordCommand>(model);
        var result = await _sender.Send(resetPasswordCommand);
        if (result.IsSuccess)
        {
            return RedirectToAction("ResetPasswordConfirmation");
        }

        HandleErrors(result);
        return View(model);
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
}