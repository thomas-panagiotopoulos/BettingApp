using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Wallet
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWalletsService _walletsSvc;

        public IndexModel(ILogger<IndexModel> logger,
                          IWalletsService walletsSvc)
        {
            _logger = logger;
            _walletsSvc = walletsSvc;
        }

        public WalletPreview Wallet { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGet()
        {
            PageNumber = PageNumber > 0 ? PageNumber : 1;

            TotalPages = await _walletsSvc.GetTransactionsPagesCount();

            Wallet = await _walletsSvc.GetWalletPreview();

            if (PageNumber <= TotalPages)
            {
                Transactions = await _walletsSvc.GetTransactionsPage(PageNumber);
            }
            else if (PageNumber > TotalPages && TotalPages > 0)
            {
                // if given PageNumber is bigger than TotalPages, then request the last page
                PageNumber = TotalPages;
                Transactions = await _walletsSvc.GetTransactionsPage(PageNumber);
            }

            return Page();
        }
    }
}
