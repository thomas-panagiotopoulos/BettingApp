using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Models
{
    public class Notification
    {
        public string Id { get; set; }

        public string GamblerId { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }
    }
}
