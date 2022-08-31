using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Account
{
    public class RegisterModel : PageModel
    {
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("/Index");
            var request = HttpContext.Request;
            var uri = request.Scheme + "://" + request.Host.Value + returnUrl;
            
            
            //var props = new AuthenticationProperties();
            //props.RedirectUri = returnUrl;
            //props.SetParameter("registerFirst", true);
            //await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, props);

            return Redirect($"https://localhost:5000/Auth/Register?returnUrl={uri}");
        }
    }
}
