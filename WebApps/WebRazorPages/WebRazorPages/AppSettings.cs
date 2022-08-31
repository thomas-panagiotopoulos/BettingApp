using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages
{
    public class AppSettings
    {
        public string SportsbookUrl { get; set; }
        public string BetslipsUrl { get; set; }
        public string BettingUrl { get; set; }
        public string WalletsUrl { get; set; }
        public string NotificationsUrl { get; set; }
        public string SportsbookSignalrHubUrl { get; set; }
        public string BetslipsSignalrHubUrl { get; set; }
        public string BettingSignalrHubUrl { get; set; }
        public string GamblingUrl { get; set; } // api gateway
        public string WalletsAndNotificationsUrl { get; set; } // api gateway
        public string GamblingUrlExternal { get; set; } // api gateway
        public string WalletsAndNotificationsUrlExternal { get; set; } // api gateway
        public bool IsContainerEnv { get; set; }

    }
}
