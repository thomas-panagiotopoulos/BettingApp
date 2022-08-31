using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Bets
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IBettingService _bettingSvc;

        public IndexModel(IBettingService bettingSvc)
        {
            _bettingSvc = bettingSvc;
        }

        public IEnumerable<BetPreview> BetPreviews { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            PageNumber = PageNumber > 0 ? PageNumber : 1;

            TotalPages = await _bettingSvc.GetBetPreviewsPagesCount();

            if(PageNumber <= TotalPages)
            {
                BetPreviews = await _bettingSvc.GetBetPreviewsPage(PageNumber);
            }
            else if(PageNumber > TotalPages && TotalPages > 0)
            {
                // if given PageNumber is bigger than TotalPages, then request the last page
                PageNumber = TotalPages;
                BetPreviews = await _bettingSvc.GetBetPreviewsPage(PageNumber);
            }

            ViewData["ReturnPage"] = PageNumber;

            return Page();
        }
    }
}
