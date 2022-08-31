using BettingApp.Services.Notifications.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Model
{
    public interface INotificationsRepository : IRepository
    {
        Notification AddNotification(Notification notification);
        Task<List<Notification>> GetAllNotificationsByGamblerIdAsync(string gamblerId);
        Task<List<Notification>> GetUnreadNotificationsByGamblerIdAsync(string gamblerId);
        Task<List<Notification>> GetNotificationsPageByGamblerIdAsync(string gamblerId, int pageNumber);
        Task<List<Notification>> GetUnreadNotificationsPageByGamblerIdAsync(string gamblerId, int pageNumber);
        Task<bool> ExistUnreadNotifications(string gamblerId);
        Task<int> GetNotificationsPagesCountAsync(string gamblerId);
    }
}
