using BettingApp.WebApps.WebRazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public interface INotificationsService
    {
        Task<IEnumerable<NotificationPreview>> GetNotificationsPreview();
        Task<IEnumerable<Notification>> GetNotificationsPage(int pageNumber);
        Task<IEnumerable<Notification>> ReadNotificationsPage(int pageNumber);
        Task<bool> MarkAsReadAllNotifications();
        Task<int> GetNotificationsPagesCount();
    }
}
