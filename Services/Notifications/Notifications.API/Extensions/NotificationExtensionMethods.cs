using BettingApp.Services.Notifications.API.DTOs;
using BettingApp.Services.Notifications.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Extensions
{
    public static class NotificationExtensionMethods
    {
        public static NotificationDTO ToNotificationDTO(this Notification notification)
        {
            return new NotificationDTO()
            {
                Id = notification.Id,
                GamblerId = notification.GamblerId,
                DateTimeCreated = notification.DateTimeCreated,
                Title = notification.Title,
                Description = notification.Description,
                IsRead = notification.IsRead,
            };
        }

        public static NotificationPreviewDTO ToNotificationPreviewDTO(this Notification notification)
        {
            return new NotificationPreviewDTO()
            {
                Id = notification.Id,
                GamblerId = notification.GamblerId,
                TimeSinceCreation = (DateTime.UtcNow.AddHours(2) - notification.DateTimeCreated).TotalMinutes < 60
                                    ? Math.Truncate((DateTime.UtcNow.AddHours(2) - notification.DateTimeCreated).TotalMinutes).ToString() + " m"
                                    : Math.Truncate((DateTime.UtcNow.AddHours(2) - notification.DateTimeCreated).TotalHours).ToString() + " h",
                Title = notification.Title,
                DescriptionPreview = notification.Description.Length <= 20 ? notification.Description : notification.Description.Substring(0, 20) + "...",
                IsRead = notification.IsRead,
            };
        }
    }
}
