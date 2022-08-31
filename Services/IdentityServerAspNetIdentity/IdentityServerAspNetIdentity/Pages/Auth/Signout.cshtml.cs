using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IdentityServerAspNetIdentity.Pages
{
    
    public class SignoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly ILogger<AppUser> _logger;

        public SignoutModel(SignInManager<AppUser> signInManager,
                           UserManager<AppUser> userManager,
                           IIdentityServerInteractionService interactionService,
                           ILogger<AppUser> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
            _logger = logger;
        }

       public async Task<IActionResult> OnGetAsync(string logoutId)
       {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return LocalRedirect("~/Index");
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri);
       }
       
       /*public async Task<IActionResult> OnGetAsync()
       {
            // if no user is signed in, redirect to Index
            if (!_signInManager.IsSignedIn(User))
            {
                return LocalRedirect("/Index");
            }
       
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User signed out succesfully.");
       
            return Page();
        }*/
    }
}
