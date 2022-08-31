using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Identity.API
{
    public class AppSettings
    {
        public string WebRazorPagesClient { get; set; }

        public bool IsContainerEnv { get; set; }
    }
}
