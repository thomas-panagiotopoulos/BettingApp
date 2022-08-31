using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Bets
{
    public class BetModel : PageModel
    {
        private readonly IBettingService _bettingSvc;

        public BetModel(IBettingService bettingSvc)
        {
            _bettingSvc = bettingSvc;
        }

        public Bet Bet { get; set; }
        public int ReturnPage { get; set; }
        public async Task<IActionResult> OnGet(string id, int returnPage)
        {
            ReturnPage = returnPage > 0 ? returnPage : 1;

            Bet = await _bettingSvc.GetBet(id);

            return Page();
        }

        public async Task<IActionResult> OnPostCancelBetAsync(string id, int returnPage)
        {
            returnPage = returnPage > 0 ? returnPage : 1;

            var returnUrl = Url.Content($"/Dashboard/Bets/Bet?id={id}&returnPage={returnPage}");

            return LocalRedirect(returnUrl);
        }
    }
}
