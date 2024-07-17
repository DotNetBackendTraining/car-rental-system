using System.Text.Json;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CarRentalSystem.Web.Services;

public class UserNotificationService :
    IUserNotificationService,
    INotificationHandler<UserNotification>
{
    private readonly ITempDataDictionary _tempData;

    public UserNotificationService(
        ITempDataDictionaryFactory tempDataDictionaryFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _tempData = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
    }

    public Task Handle(UserNotification notification, CancellationToken cancellationToken)
    {
        var notifications = new List<UserNotification>();
        if (_tempData.TryGetValue("Notifications", out var value))
        {
            notifications = JsonSerializer.Deserialize<List<UserNotification>>(value!.ToString());
        }

        notifications!.Add(notification);
        _tempData["Notifications"] = JsonSerializer.Serialize(notifications);

        return Task.CompletedTask;
    }

    public IEnumerable<UserNotification> GetNotifications()
    {
        if (_tempData.TryGetValue("Notifications", out var value))
        {
            return JsonSerializer.Deserialize<List<UserNotification>>(value!.ToString()) ?? [];
        }

        return new List<UserNotification>();
    }

    public void ClearNotifications()
    {
        _tempData.Remove("Notifications");
    }
}