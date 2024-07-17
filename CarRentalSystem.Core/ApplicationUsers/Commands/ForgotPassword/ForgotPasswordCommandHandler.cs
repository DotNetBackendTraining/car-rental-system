using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Interfaces.Messaging;
using CarRentalSystem.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand>
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public ForgotPasswordCommandHandler(
        IMediator mediator,
        UserManager<ApplicationUser> userManager,
        IEmailConfirmationService emailConfirmationService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _emailConfirmationService = emailConfirmationService;
    }

    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            var error = DomainErrors.UserWithEmailNotFound;
            await _mediator.Publish(UserNotification.Error(error), cancellationToken);
            return Result.Failure(error);
        }

        await _emailConfirmationService.SendPasswordResetEmailAsync(user);
        await _mediator.Publish(UserNotification.Info("Password reset email has been sent."), cancellationToken);

        return Result.Success();
    }
}