using BettingApp.Services.Notifications.API.DTOs;
using BettingApp.Services.Notifications.API.Extensions;
using BettingApp.Services.Notifications.API.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly INotificationsRepository _repository;

        public HomeController(INotificationsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to Notifications!");
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationsPageAsync([FromQuery] int pageNumber)
        {
            // normally at this point we get gamblerId from IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var notificationsPage = await _repository.GetNotificationsPageByGamblerIdAsync(gamblerId, pageNumber);
            var notificationDTOs = notificationsPage?.Select(n => n.ToNotificationDTO());
            return Ok(notificationDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationsPreviewAsync()
        {
            // normally at this point we get gamblerId from IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var notificationsPage = await _repository.GetNotificationsPageByGamblerIdAsync(gamblerId, 1);
            var notificationPreviewDTOs = notificationsPage?.Take(5).Select(n => n.ToNotificationPreviewDTO());
            return Ok(notificationPreviewDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> ReadNotificationsPageAsync([FromQuery] int pageNumber)
        {
            // normally at this point we get gamblerId from IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var notificationsPage = await _repository.GetNotificationsPageByGamblerIdAsync(gamblerId, pageNumber);
            
            // if we convert notificationsPages to notificationDTOs this way, then when we mark notificationsPage 
            // as read, the notificationDTOs are also marked as read. so it doesn't work the way we expect to.
            //var notificationDTOs = notificationsPage?.Select(n => n.ToNotificationDTO());
            
            var notificationDTOs = new List<NotificationDTO>();
            foreach(var notification in notificationsPage)
            {
                notificationDTOs.Add(notification.ToNotificationDTO());
            }

            if (notificationsPage.Any())
            {
                notificationsPage.ForEach(n => n.MarkAsRead());
                await _repository.UnitOfWork.SaveChangesAsync();
            }
            return Ok(notificationDTOs);
        }

        public async Task<IActionResult> GetNotificationsPagesCount()
        {
            // normally at this point we get gamblerId from IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var pagesCount = await _repository.GetNotificationsPagesCountAsync(gamblerId);
            return Ok(pagesCount);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsReadAllNotificationsAsync()
        {
            // normally at this point we get gamblerId from IdentityService
            var gamblerId = "ac97804c-f777-4dd5-b33e-735b42f81dcf";

            var unreadNotifications = await _repository.GetUnreadNotificationsByGamblerIdAsync(gamblerId);
            if (unreadNotifications.Any())
            {
                unreadNotifications.ForEach(n => n.MarkAsRead());
                await _repository.UnitOfWork.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ExistUnreadNotifications()
        {
            // normally at this point we get gamblerId from IdentityService
            var gamblerId = "ac97804c-f777-4dd5-b33e-735b42f81dcf";
            var exist = await _repository.ExistUnreadNotifications(gamblerId);
            return Ok(exist);
        }

        // =============================

        [HttpPost]
        [Route("{gamblerId}")]
        public async Task<IActionResult> CreateRandomNotificationsAsync(string gamblerId, [FromQuery]int count)
        {
            for(var i = 0; i<count; i++)
            {
                var notification = new Notification(gamblerId, "Random Title", RandomString(25));
                _repository.AddNotification(notification);
                await _repository.UnitOfWork.SaveChangesAsync();
                await Task.Delay(1000);
            }

            return Ok();
        }

        private static string RandomString(int length)
        {
            Random random = new Random();

            string chars = "abcdefghijklmnopqrstuvwxyz" +
                           "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                           "0123456789 ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
