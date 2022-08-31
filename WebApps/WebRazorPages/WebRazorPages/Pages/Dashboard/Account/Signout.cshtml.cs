using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Account
{
    public class SignoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Index");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            var homeUrl = Url.Content("/Index");
            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = homeUrl });
        }

        public async Task<IActionResult> OnPost()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Index");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            var homeUrl = Url.Content("/Index");
            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = homeUrl });
        }
    }
}
