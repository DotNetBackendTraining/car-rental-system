using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces.Messaging;
using CarRentalSystem.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;

    public LoginUserCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IMediator mediator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.Failure(DomainErrors.UserWithEmailNotFound);
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            var error = DomainErrors.UserEmailNotConfirmed;
            await _mediator.Publish(UserNotification.Error(error), cancellationToken);
            return Result.Failure(DomainErrors.UserEmailNotConfirmed);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            request.Password,
            request.RememberMe,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Result.Failure(DomainErrors.UserCredentialsInvalid);
        }

        await _mediator.Publish(UserNotification.Success("Login successful!"), cancellationToken);
        return Result.Success();
    }
}