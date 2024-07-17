using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces.Messaging;
using CarRentalSystem.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.LogoutUserCommand;

public class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMediator _mediator;

    public LogoutUserCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        IMediator mediator)
    {
        _signInManager = signInManager;
        _mediator = mediator;
    }

    public async Task<Result> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
        await _mediator.Publish(UserNotification.Info("You've been logged out"), cancellationToken);

        return Result.Success();
    }
}