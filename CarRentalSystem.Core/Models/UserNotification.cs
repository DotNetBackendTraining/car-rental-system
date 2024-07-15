using MediatR;

namespace CarRentalSystem.Core.Models;

public class UserNotification : INotification
{
    public string Message { get; set; } = string.Empty;
    public UserNotificationType Type { get; set; }
}

public enum UserNotificationType
{
    Success,
    Error,
    Info,
    Warning
}