using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BettingApp.WebApps.WebRazorPages.Pages.Betslip
{
    public class CheckoutSuccessModel : PageModel
    {
        private readonly ILogger<CheckoutSuccessModel> _logger;
        private readonly IBettingService _bettingSvc;

        public CheckoutSuccessModel(ILogger<CheckoutSuccessModel> logger,
                                    IBettingService bettingSvc)
        {
            _logger = logger;
            _bettingSvc = bettingSvc;
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

            RequestFound = await _bettingSvc.RequestExists(RequestId);

            return Page();
        }
    }
}
