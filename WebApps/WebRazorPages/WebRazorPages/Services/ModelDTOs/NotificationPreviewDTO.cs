using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services.ModelDTOs
{
    public class NotificationPreviewDTO
    {
        public string Id { get; set; }
        public string GamblerId { get; set; }
        public string TimeSinceCreation { get; set; }
        public string Title { get; set; }
        public string DescriptionPreview { get; set; }
        public bool IsRead { get; set; }
    }
}
