using CarRentalSystem.Core.Interfaces.Messaging;

namespace CarRentalSystem.Core.ApplicationUsers.Commands.LoginUser;

public class LoginUserCommand : UserCredentialsCommandBase, ICommand
{
    public bool RememberMe { get; set; }
}