namespace CarRentalSystem.Web.ViewModels;

public class ResetPasswordViewModel : PasswordFieldViewModel
{
    public string UserId { get; set; }
    public string Token { get; set; }
}