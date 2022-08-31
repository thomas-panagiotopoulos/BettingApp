using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.Services.Identity.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.Services.Identity.API.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRedirectService _redirectSvc;
        public IndexModel(IRedirectService redirectSvc)
        {
            _redirectSvc = redirectSvc;
        }

        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Index");
            ReturnUrl = returnUrl;

            return Page();
        }

        public IActionResult OnPostReturnToOriginalApplication(string returnUrl = null)
        {
            if (returnUrl != null)
                return Redirect(_redirectSvc.ExtractRedirectUriFromReturnUrl(returnUrl));
            else
                return Page();
        }
    }
}
