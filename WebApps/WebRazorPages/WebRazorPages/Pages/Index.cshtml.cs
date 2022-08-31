using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.WebApps.WebRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISportsbookService _sportsbookSvc;
        private readonly IBetslipsService _betslipsSvc;
        private readonly IWalletsService _walletsSvc;
        private readonly IHttpContextAccessor _httpContext;

        public IndexModel(ISportsbookService sportsbookSvc, 
            IBetslipsService betslipsSvc,
            IWalletsService walletsSvc,
            IHttpContextAccessor httpContext)
        {
            _sportsbookSvc = sportsbookSvc;
            _betslipsSvc = betslipsSvc;
            _walletsSvc = walletsSvc;
            _httpContext = httpContext;
        }

        public List<Match> Matches { get; set; }
        public List<League> Leagues { get; set; }
        public List<MatchResult> MatchResults { get; set; }
        public Models.Betslip Betslip { get; set; } // clarify which Betslip we "mean" because the Betslip page has
                                                    // a namespace called Betslip too

        [BindProperty(SupportsGet = true)]
        public DateTime Date { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ReturnUrl"] = Url.Content("~/Index");
            Date = DateTime.UtcNow.AddHours(2);

            Matches = await _sportsbookSvc.GetMatchesByDate(DateTime.UtcNow.AddHours(2));
            Matches = Matches?.OrderBy(m => m.LeagueId).ThenBy(m => m.KickoffDateTime).ToList();

            if (User.Identity.IsAuthenticated)
            {
                //var wallet = await _walletsSvc.GetWalletPreview();
                //ViewData["WalletBalance"] = wallet.Balance;
                Betslip = await _betslipsSvc.GetBetslip();
            }

            if (Matches == null || !Matches.Any())
                return Page();

            Leagues = await _sportsbookSvc.GetLeaguesByDate(DateTime.UtcNow.AddHours(2));
            MatchResults = await _sportsbookSvc.GetMatchResults();
            
            return Page();
        }

        public async Task<IActionResult> OnGetLoadMatchesByDateAsync()
        {
            // If Date has default value (which means it wasn't provided by user input) then assign the current DateTime
            Date = Date == DateTime.MinValue ? DateTime.UtcNow.AddHours(2) : Date;
            ViewData["ReturnUrl"] = Url.Content("~/Index/LoadMatchesByDate?Date="+Date.ToString("yyyy-MM-dd"));

            Matches = await _sportsbookSvc.GetMatchesByDate(Date);
            Matches = Matches?.OrderBy(m => m.LeagueId).ThenBy(m => m.KickoffDateTime).ToList();

            if (User.Identity.IsAuthenticated)
            {
                //var wallet = await _walletsSvc.GetWalletPreview();
                //ViewData["WalletBalance"] = wallet.Balance;
                Betslip = await _betslipsSvc.GetBetslip();
            }

            if (Matches == null || !Matches.Any()) 
                return Page();

            Leagues = await _sportsbookSvc.GetLeaguesByDate(Date);
            MatchResults = await _sportsbookSvc.GetMatchResults();

            return Page();
        }

    }
}
