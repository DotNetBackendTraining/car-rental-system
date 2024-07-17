using CarRentalSystem.Core.Interfaces.Messaging;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.ForgotPassword;

public class ForgotPasswordCommand : ICommand
{
    public string Email { get; set; } = string.Empty;
}