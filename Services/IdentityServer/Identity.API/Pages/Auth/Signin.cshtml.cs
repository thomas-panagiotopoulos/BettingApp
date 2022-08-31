using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.Services.Identity.API.Models;
using BettingApp.Services.Identity.API.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BettingApp.Services.Identity.API.Pages.Auth
{
    public class SigninModel : PageModel
    {
        private readonly ILogger<SigninModel> _logger;
        private readonly ILoginService<ApplicationUser> _loginService;
        private readonly IConfiguration _configuration;
        private readonly IIdentityServerInteractionService _interaction;

        public SigninModel(ILoginService<ApplicationUser> loginService,
                        IIdentityServerInteractionService interaction,
                        IConfiguration configuration,
                        ILogger<SigninModel> logger)
        {
            _loginService = loginService;
            _interaction = interaction;
            _configuration = configuration;
            _logger = logger;

        }

        [BindProperty]
        public SigninInputModel Input { get; set; }

        public string ReturnUrl { get; set; }


        public async Task<IActionResult> OnGet(string returnUrl = null, bool registerFirst = false)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;

            if (registerFirst)
                RedirectToPage("/Auth/Register", new { returnUrl = returnUrl });

            // if user is already signed in, redirect him to relevant page
            if (_loginService.IsSignedIn(User))
            {
                _logger.LogInformation("A user is already signed in.");
                return Redirect(returnUrl);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;

            // if user is already signed in, redirect him to relevant page
            if (_loginService.IsSignedIn(User))
            {
                _logger.LogInformation("A user is already signed in.");
                return Redirect(returnUrl);
            }

            if (ModelState.IsValid)
            {
                var user = await _loginService.FindByUsername(Input.Email);

                if (await _loginService.ValidateCredentials(user, Input.Password))
                {
                    var tokenLifetime = _configuration.GetValue("TokenLifetimeMinutes", 120);

                    var props = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2).AddMinutes(tokenLifetime),
                        AllowRefresh = true,
                        RedirectUri = returnUrl,
                    };

                    await _loginService.SignInAsync(user, props);

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                    if (_interaction.IsValidReturnUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return Redirect("~/");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return Page();
        }
    }

    public class SigninInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
