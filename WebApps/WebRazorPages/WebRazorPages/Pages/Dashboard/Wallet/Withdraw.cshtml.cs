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
    public class WithdrawModel : PageModel
    {
        private readonly ILogger<WithdrawModel> _logger;
        private readonly IWalletsService _walletsSvc;

        public WithdrawModel(ILogger<WithdrawModel> logger,
                          IWalletsService walletsSvc)
        {
            _logger = logger;
            _walletsSvc = walletsSvc;
        }

        [BindProperty]
        public string IBAN { get; set; }
        [BindProperty]
        public string BankAccountHolder { get; set; }
        [BindProperty]
        public decimal WithdrawAmount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ReturnPage { get; set; }

        public decimal CurrentBalance { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ReturnPage = ReturnPage > 0 ? ReturnPage : 1;

            var wallet = await _walletsSvc.GetWalletPreview();

            if(wallet != null)
            {
                CurrentBalance = wallet.Balance;
            }
            else
            {
                // if wallet is "unreachable", assign an invalid value to handle the visualisation
                CurrentBalance = -1m;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //_logger.LogInformation($"WithdrawAmount: {WithdrawAmount}\n IBAN: {IBAN}\n " +
            //    $"BankAccountHolder: {BankAccountHolder}\n ReturnPage: {ReturnPage}");
            //
            //ReturnPage = ReturnPage > 0 ? ReturnPage : 1;
            //
            //return Page();
            var withdrawRequestId = await _walletsSvc.RequestWithdraw(WithdrawAmount, IBAN, BankAccountHolder);
            
            if (withdrawRequestId == null)
            {
                ReturnPage = ReturnPage > 0 ? ReturnPage : 1;
                return Page();
            }
            
            return RedirectToPage("/Dashboard/Wallet/WithdrawSuccess", new { RequestId = withdrawRequestId });
        }
    }
}
