using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.Services.Identity.API.Pages.Auth
{
    public class SignoutModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;

        public SignoutModel(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        public async Task<IActionResult> OnGet(string logoutId, string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                // if the user is not authenticated, redirect him to home page
                if(returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/Index");
                }   
            }

            // delete authentication cookie
            await HttpContext.SignOutAsync();

            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logout.PostLogoutRedirectUri))
            {
                return LocalRedirect("~/Index");
            }

            return Redirect(logout?.PostLogoutRedirectUri);
        }
    }
}
