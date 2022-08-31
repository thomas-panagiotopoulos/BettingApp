using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INotificationsService _notificationsSvc;

        public IndexModel(ILogger<IndexModel> logger,
                          INotificationsService notifiationsSvc)
        {
            _logger = logger;
            _notificationsSvc = notifiationsSvc;
        }

        public IEnumerable<Notification> Notifications { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            PageNumber = PageNumber > 0 ? PageNumber : 1;

            TotalPages = await _notificationsSvc.GetNotificationsPagesCount();

            if (PageNumber <= TotalPages)
            {
                Notifications = await _notificationsSvc.ReadNotificationsPage(PageNumber);
                //Notifications = await _notificationsSvc.GetNotificationsPage(PageNumber);
            }
            else if (PageNumber > TotalPages && TotalPages > 0)
            {
                // if given PageNumber is bigger than TotalPages, then request the last page
                PageNumber = TotalPages;
                Notifications = await _notificationsSvc.ReadNotificationsPage(PageNumber);
                //Notifications = await _notificationsSvc.GetNotificationsPage(PageNumber);
            }

            return Page();
        }
    }
}
