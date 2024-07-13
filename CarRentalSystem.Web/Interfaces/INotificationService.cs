using CarRentalSystem.Web.Models;

namespace CarRentalSystem.Web.Interfaces;

public interface INotificationService
{
    void AddNotification(string message, NotificationType type);

    IEnumerable<Notification> GetNotifications();

    void ClearNotifications();
}