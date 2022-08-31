using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BettingApp.WebApps.WebRazorPages.Pages.Betslip
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBetslipsService _betslipsSvc;
        private readonly ISportsbookService _sportsbookSvc;
        private readonly IHttpContextAccessor _httpContext;

        public IndexModel(ILogger<IndexModel> logger,
                        IBetslipsService betslipsSvc, 
                        ISportsbookService sportsbookSvc, 
                        IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _betslipsSvc = betslipsSvc;
            _sportsbookSvc = sportsbookSvc;
            _httpContext = httpContext;
        }

        public Models.Betslip Betslip { get; set; }
        public decimal WalletBalance { get; set; }

        public async Task<IActionResult> OnGet()
        {
            WalletBalance = await _betslipsSvc.GetWalletBalance();
            Betslip = await _betslipsSvc.GetBetslip();

            ViewData["ReturnUrl"] = Url.Content("/Betslip");
            return Page();
        }


        public async Task<IActionResult> OnPostRequestAddSelectionAsync(string matchId, int matchResultId, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("/Index");

            var canAddSelection = await _betslipsSvc.CheckAddSelection(matchId, matchResultId);

            if (canAddSelection)
            {
                // requestId is used as latestAdditionId on Betslip
                var requestId = await _sportsbookSvc.RequestToAddSelection(matchId, matchResultId);
            
                var additionVerified = await _betslipsSvc.VerifyLatestAddition(requestId);
                // custom retry policy: if latestAdditionId is not found in Betslip, retry verification 4 more times
                var tries = 0;
                while (!additionVerified && tries<=4)
                {
                    Thread.Sleep(200 + 250*tries);
                    tries++;
                    additionVerified = await _betslipsSvc.VerifyLatestAddition(requestId);
                }
            }

            return LocalRedirect(returnUrl);
        }


        public async Task<IActionResult> OnPostRemoveSelection(string selectionId, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("/Index");

            var result = await _betslipsSvc.RemoveSelection(selectionId);

            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> OnPostClearBetslipAsync(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("/Index");

            var result = await _betslipsSvc.ClearBetslip();

            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> OnPostUpdateWageredAmountAsync(decimal wageredAmount, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("/Index");

            var result = await _betslipsSvc.UpdateWageredAmount(wageredAmount);

            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> OnPostCheckoutBetslipAsync()
        {
            var checkoutResponseId = await _betslipsSvc.Checkout();
            
            if(checkoutResponseId == null)
            {
                return LocalRedirect("/Betslip/Index");
            }
            
            return RedirectToPage("../Betslip/CheckoutSuccess", new { RequestId = checkoutResponseId });
        }
    }
}
