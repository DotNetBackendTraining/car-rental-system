using CarRentalSystem.Core.Shared;

namespace CarRentalSystem.Core.Interfaces;

public interface IUserNotificationService
{
    IEnumerable<UserNotification> GetNotifications();

    void ClearNotifications();
}