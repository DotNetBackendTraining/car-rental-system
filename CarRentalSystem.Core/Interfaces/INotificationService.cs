using CarRentalSystem.Core.Models;

namespace CarRentalSystem.Core.Interfaces;

public interface INotificationService
{
    void AddNotification(string message, NotificationType type);

    IEnumerable<Notification> GetNotifications();

    void ClearNotifications();
}