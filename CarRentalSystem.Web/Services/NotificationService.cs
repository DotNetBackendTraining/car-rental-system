using System.Text.Json;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Models;
using CarRentalSystem.Web.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CarRentalSystem.Web.Services;

public class NotificationService : INotificationService
{
    private readonly ITempDataDictionary _tempData;

    public NotificationService(
        ITempDataDictionaryFactory tempDataDictionaryFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _tempData = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
    }

    public void AddNotification(string message, NotificationType type)
    {
        var notifications = new List<Notification>();
        if (_tempData.TryGetValue("Notifications", out var value))
        {
            notifications = JsonSerializer.Deserialize<List<Notification>>(value!.ToString());
        }

        notifications!.Add(new Notification { Message = message, Type = type });
        _tempData["Notifications"] = JsonSerializer.Serialize(notifications);
    }

    public IEnumerable<Notification> GetNotifications()
    {
        if (_tempData.TryGetValue("Notifications", out var value))
        {
            return JsonSerializer.Deserialize<List<Notification>>(value!.ToString()) ?? [];
        }

        return new List<Notification>();
    }

    public void ClearNotifications()
    {
        _tempData.Remove("Notifications");
    }
}