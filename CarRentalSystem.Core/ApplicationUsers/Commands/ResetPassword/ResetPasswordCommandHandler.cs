using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces.Messaging;
using CarRentalSystem.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.ResetPassword;

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordCommandHandler(
        IMediator mediator,
        UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return Result.Failure(DomainErrors.UserWithIdNotFound);
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        if (result.Succeeded)
        {
            await _mediator.Publish(
                UserNotification.Success("Your password has been reset successfully."),
                cancellationToken);
        }

        return Result.Success();
    }
}