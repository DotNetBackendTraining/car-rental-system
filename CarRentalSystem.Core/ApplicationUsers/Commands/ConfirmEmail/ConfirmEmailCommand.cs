using CarRentalSystem.Core.Interfaces.Messaging;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.ConfirmEmail;

public class ConfirmEmailCommand : ICommand
{
    public string UserId { get; set; }
    public string Token { get; set; }
}