using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Wallet
{
    public class TopUpModel : PageModel
    {
        private readonly ILogger<TopUpModel> _logger;
        private readonly IWalletsService _walletsSvc;

        public TopUpModel(ILogger<TopUpModel> logger,
                          IWalletsService walletsSvc)
        {
            _logger = logger;
            _walletsSvc = walletsSvc;
        }

        [BindProperty]
        public Card Card { get; set; }
        [BindProperty]
        public decimal TopUpAmount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ReturnPage { get; set; }

        public decimal CurrentBalance { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ReturnPage = ReturnPage > 0 ? ReturnPage : 1;

            var wallet = await _walletsSvc.GetWalletPreview();

            if (wallet != null)
            {
                CurrentBalance = wallet.Balance;
            }
            else
            {
                // if wallet couldn't be "loaded", assign an invalid value to CurrentBalance to handle the visualisation
                CurrentBalance = -1m;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //_logger.LogInformation($"Card.Number: {Card.CardNumber} \n" +
            //    $"Card.CardholderName: {Card.CardHolderName} \n" +
            //    $"Card.CVV: {Card.SecurityNumber} \n" +
            //    $"Card.Expiration: {Card.ExpirationDateMM}/{Card.ExpirationDateYY} \n" +
            //    $"TopUpAmount: {TopUpAmount} \n" +
            //    $"ReturnPage: {ReturnPage}");

            var topUpRequestId = await _walletsSvc.RequestTopUp(TopUpAmount, Card);
            
            if (topUpRequestId == null)
            {
                ReturnPage = ReturnPage > 0 ? ReturnPage : 1;
                return Page();
            }
            
            return RedirectToPage("/Dashboard/Wallet/TopUpSuccess", new { RequestId = topUpRequestId });
        }
    }
}
