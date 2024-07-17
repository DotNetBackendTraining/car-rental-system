using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces.Messaging;
using CarRentalSystem.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmailCommandHandler(
        IMediator mediator,
        UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return Result.Failure(DomainErrors.UserWithIdNotFound);
        }

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        if (result.Succeeded)
        {
            await _mediator.Publish(UserNotification.Success("Email confirmed successfully."), cancellationToken);
        }

        return Result.Success();
    }
}