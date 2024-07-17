using AutoMapper;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Interfaces.Messaging;
using CarRentalSystem.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public RegisterUserCommandHandler(
        IMapper mapper,
        IMediator mediator,
        UserManager<ApplicationUser> userManager,
        IEmailConfirmationService emailConfirmationService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _userManager = userManager;
        _emailConfirmationService = emailConfirmationService;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<ApplicationUser>(request);

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return ValidationResult.WithErrors(result.Errors
                .Select(e => new Error(e.Code, e.Description)).ToArray());
        }

        await _emailConfirmationService.SendConfirmationEmailAsync(user);
        await _mediator.Publish(
            UserNotification.Success("Registration successful! Please check your email to confirm your account."),
            cancellationToken);

        return Result.Success();
    }
}