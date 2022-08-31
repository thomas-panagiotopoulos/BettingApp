using AutoMapper;
using BettingApp.WebApps.WebRazorPages.Infrastructure;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services.ModelDTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationsService> _logger;
        private readonly string _notificationsUrl;

        public NotificationsService(IOptions<AppSettings> settings,
                                    HttpClient apiClient,
                                    IMapper mapper,
                                    ILogger<NotificationsService> logger)
        {
            _settings = settings;
            _apiClient = apiClient;
            _mapper = mapper;
            _logger = logger;

            if (_settings.Value.IsContainerEnv == true)
            {
                _notificationsUrl = _settings.Value.WalletsAndNotificationsUrl;
            }
            else
            {
                _notificationsUrl = _settings.Value.NotificationsUrl;
            }
        }

        public async Task<IEnumerable<NotificationPreview>> GetNotificationsPreview()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Notifications.GetNotificationsPreview(_notificationsUrl)
                                                     : APIv0.Notifications.GetNotificationsPreview(_notificationsUrl);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var notificationPreviewDTOs = JsonConvert.DeserializeObject<List<NotificationPreviewDTO>>(responseString);
                var notificationsPreview = notificationPreviewDTOs?.Select(npDTO => _mapper.Map<NotificationPreview>(npDTO));

                return notificationsPreview;
            }
            return null;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsPage(int pageNumber)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Notifications.GetNotificationsPage(_notificationsUrl, pageNumber)
                                                     : APIv0.Notifications.GetNotificationsPage(_notificationsUrl, pageNumber);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var notificationDTOs = JsonConvert.DeserializeObject<List<NotificationDTO>>(responseString);
                var notifications = notificationDTOs?.Select(nDTO => _mapper.Map<Notification>(nDTO));

                return notifications;
            }
            return null;
        }

        public async Task<IEnumerable<Notification>> ReadNotificationsPage(int pageNumber)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Notifications.ReadNotificationsPage(_notificationsUrl, pageNumber)
                                                     : APIv0.Notifications.ReadNotificationsPage(_notificationsUrl, pageNumber);
            var response = await _apiClient.PostAsync(uri, null);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var notificationDTOs = JsonConvert.DeserializeObject<List<NotificationDTO>>(responseString);
                var notifications = notificationDTOs?.Select(nDTO => _mapper.Map<Notification>(nDTO));

                return notifications;
            }
            return null;
        }

        public async Task<bool> MarkAsReadAllNotifications()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Notifications.MarkAsReadAllNotifications(_notificationsUrl)
                                                     : APIv0.Notifications.MarkAsReadAllNotifications(_notificationsUrl);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<int> GetNotificationsPagesCount()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Notifications.GetNotificationsPagesCount(_notificationsUrl)
                                                     : APIv0.Notifications.GetNotificationsPagesCount(_notificationsUrl);
            var response = await _apiClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();

            var notificationsPagesCount = JsonConvert.DeserializeObject<int>(responseString);

            return notificationsPagesCount;
        }
    }
}
