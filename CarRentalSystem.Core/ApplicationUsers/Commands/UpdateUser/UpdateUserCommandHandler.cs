using AutoMapper;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Interfaces.Messaging;
using CarRentalSystem.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserAccessorService _userAccessorService;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public UpdateUserCommandHandler(
        IMapper mapper,
        IMediator mediator,
        UserManager<ApplicationUser> userManager,
        IUserAccessorService userAccessorService,
        IEmailConfirmationService emailConfirmationService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _userManager = userManager;
        _userAccessorService = userAccessorService;
        _emailConfirmationService = emailConfirmationService;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userAccessorService.GetCurrentUserAsync();
        if (user == null)
        {
            return Result.Failure(DomainErrors.UserWithIdNotFound);
        }

        var emailChanged = !string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase);
        _mapper.Map(request, user);

        if (emailChanged)
        {
            user.EmailConfirmed = false;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return ValidationResult.WithErrors(result.Errors
                .Select(e => new Error(e.Code, e.Description)).ToArray());
        }

        await _mediator.Publish(
            UserNotification.Success("Profile updated successfully."),
            cancellationToken);

        if (!emailChanged)
        {
            return Result.Success();
        }

        await _emailConfirmationService.SendConfirmationEmailAsync(user);
        await _mediator.Publish(
            UserNotification.Warning("Please check your email to confirm your account."),
            cancellationToken);

        return Result.Success();
    }
}