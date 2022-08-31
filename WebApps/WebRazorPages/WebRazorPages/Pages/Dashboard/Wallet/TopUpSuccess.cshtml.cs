using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Wallet
{
    public class TopUpSuccessModel : PageModel
    {
        private readonly ILogger<TopUpSuccessModel> _logger;
        private readonly IWalletsService _walletsSvc;

        public TopUpSuccessModel(ILogger<TopUpSuccessModel> logger,
                                IWalletsService walletsSvc)
        {
            _logger = logger;
            _walletsSvc = walletsSvc;
        }

        [BindProperty(SupportsGet = true)]
        public string RequestId { get; set; }

        public bool RequestFound { get; set; }
        public bool RequestIdValid { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Guid requestIdAsGuid;

            RequestIdValid = Guid.TryParse(RequestId, out requestIdAsGuid);

            if (!RequestIdValid)
            {
                return Page();
            }

            RequestFound = await _walletsSvc.TopUpRequestExists(RequestId);

            return Page();
        }
    }
}
