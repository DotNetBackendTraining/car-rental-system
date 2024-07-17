using MediatR;

namespace CarRentalSystem.Core.Shared;

public class UserNotification : INotification
{
    public string Message { get; set; } = string.Empty;
    public UserNotificationType Type { get; set; }

    public static UserNotification Success(string message) => new()
    {
        Message = message,
        Type = UserNotificationType.Success
    };

    public static UserNotification Error(Error error) => Error(error.Message);

    public static UserNotification Error(string message) => new()
    {
        Message = message,
        Type = UserNotificationType.Error
    };

    public static UserNotification Info(string message) => new()
    {
        Message = message,
        Type = UserNotificationType.Info
    };

    public static UserNotification Warning(string message) => new()
    {
        Message = message,
        Type = UserNotificationType.Warning
    };
}

public enum UserNotificationType
{
    Success = 0,
    Error = 1,
    Info = 2,
    Warning = 3
}