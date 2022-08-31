using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IdentityServerAspNetIdentity.Pages
{
    public class SigninModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<SigninModel> _logger;

        public SigninModel(UserManager<AppUser> userManager,
                        SignInManager<AppUser> signInManager,
                        ILogger<SigninModel> logger) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public SigninInputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        

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

        // Serves the Login page
        public IActionResult OnGet(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;

            // if user is already signed in, redirect him to relevant page
            if (_signInManager.IsSignedIn(User))
            {
                _logger.LogInformation("A user is already signed in.");
                return Redirect(returnUrl);
                //return LocalRedirect("/Auth/AlreadyLoggedIn");
            }

            
            return Page();

        }

        // Executes the Login logic
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;

            // if user is already signed in, redirect him to relevant page
            if (_signInManager.IsSignedIn(User))
            {
                _logger.LogInformation("A user is already signed in.");
                return Redirect(returnUrl);
                //return LocalRedirect("/Auth/AlreadyLoggedIn");
            }

            // if there are no validation errors, proceed to user login
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                if(user != null)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(user, Input.Password, isPersistent: false, lockoutOnFailure: false);
                    
                    if (signInResult.Succeeded)
                    {
                        // login succesfull, redirect to Index page
                        _logger.LogInformation("User succesfully logged in.");
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        // invalid login details
                        _logger.LogInformation("Invalid email/password combination.");
                        ModelState.AddModelError(string.Empty, "Invalid email/password combination.");
                        return Page();
                    }
                    
                }
                else
                {
                    // user not found 
                    _logger.LogInformation("User not found.");
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return Page();
                }

                
            }

            // if we got this far something failed, redisplay form
            _logger.LogInformation("ModelState was not valid.");
            return Page();
            
        }
    }
}
