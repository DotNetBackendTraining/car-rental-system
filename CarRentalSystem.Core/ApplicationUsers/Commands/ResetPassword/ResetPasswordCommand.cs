using CarRentalSystem.Core.Interfaces.Messaging;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.ResetPassword;

public class ResetPasswordCommand : ICommand
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
}