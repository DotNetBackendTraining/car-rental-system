namespace CarRentalSystem.Web.Models;

public class Notification
{
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
}

public enum NotificationType
{
    Success,
    Error,
    Info,
    Warning
}