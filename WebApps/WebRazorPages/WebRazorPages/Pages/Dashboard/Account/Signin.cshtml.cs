using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Account
{
    [Authorize]
    public class SigninModel : PageModel
    {
        public IActionResult OnGet(string returnUrl = null)
        {
            // whenever an unauthenticated user accesses this page, he will be challenged by
            // redirecting to Identity's Login screen

            if(returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
