using BettingApp.Services.Notifications.API.Model;
using BettingApp.Services.Notifications.API.Model.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Infrastructure.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private int _pageSize = 10;
        private readonly NotificationsContext _context;

        public NotificationsRepository(NotificationsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public Notification AddNotification(Notification notification)
        {
            var entity = _context.Notifications.Add(notification).Entity;

            return entity;
        }

        public async Task<List<Notification>> GetAllNotificationsByGamblerIdAsync(string gamblerId)
        {
            var notifications = await _context.Notifications
                                            .Where(n => n.GamblerId.Equals(gamblerId))
                                            .OrderBy(n => n.IsRead)
                                            .ThenByDescending(n => n.DateTimeCreated)
                                            .ToListAsync();

            return notifications;
        }

        public async Task<List<Notification>> GetUnreadNotificationsByGamblerIdAsync(string gamblerId)
        {
            var notifications = await _context.Notifications
                                              .Where(n => n.GamblerId.Equals(gamblerId) && !n.IsRead)
                                              .OrderBy(n => n.IsRead)
                                              .ThenByDescending(n => n.DateTimeCreated)
                                              .ToListAsync();
            return notifications;
        }

        public async Task<List<Notification>> GetNotificationsPageByGamblerIdAsync(string gamblerId, int pageNumber)
        {
            // each page consists of 10 Notifications always firtst ordered by IsRead status and then by DateTimeCreated,
            // so for example PageNumber=1 will include the latest 10 unread Notifications, PageNumber=2 will include
            // the latest 11 to 20 unread Notifications, etc.
            if (pageNumber < 1)
                return null;

            var notificationsPage = await _context.Notifications
                                            .Where(n => n.GamblerId.Equals(gamblerId))
                                            .OrderBy(n => n.IsRead)
                                            .ThenByDescending(n => n.DateTimeCreated)
                                            .Skip(pageNumber * 10 - 10)
                                            .Take(10)
                                            .ToListAsync();
            return notificationsPage;
        }

        public async Task<List<Notification>> GetUnreadNotificationsPageByGamblerIdAsync(string gamblerId, int pageNumber)
        {
            // each page consists of 10 Notifications always ordered by DateTimeCreated, so for example PageNumber=1
            // will include the latest 10 Notifications, PageNumber=2 will include the latest 11 to 20 Notifications, etc.
            if (pageNumber < 1)
                return null;

            var notificationsPage = await _context.Notifications
                                            .Where(n => n.GamblerId.Equals(gamblerId) && !n.IsRead)
                                            .OrderBy(n => n.IsRead)
                                            .ThenByDescending(n => n.DateTimeCreated)
                                            .Skip(pageNumber * 10 - 10)
                                            .Take(10)
                                            .ToListAsync();
            return notificationsPage;
        }

        public async Task<bool> ExistUnreadNotifications(string gamblerId)
        {
            return await _context.Notifications.AnyAsync(n => n.GamblerId.Equals(gamblerId) && !n.IsRead);
        }

        public async Task<int> GetNotificationsPagesCountAsync(string gamblerId)
        {
            var notifications = await _context.Notifications
                                        .Where(n => n.GamblerId.Equals(gamblerId))
                                        .ToListAsync();

            var totalNotifications = (notifications == null) ? 0 : notifications.Count();

            return (totalNotifications / _pageSize) + (totalNotifications % _pageSize > 0 ? 1 : 0);
        }
    }
}
