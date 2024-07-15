using CarRentalSystem.Core.Models;

namespace CarRentalSystem.Core.Interfaces;

public interface IUserNotificationService
{
    IEnumerable<UserNotification> GetNotifications();

    void ClearNotifications();
}