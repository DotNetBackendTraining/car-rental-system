namespace CarRentalSystem.Core.ApplicationUsers.Commands;

public abstract class UserCredentialsCommandBase
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}