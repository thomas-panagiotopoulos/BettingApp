using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IdentityServerAspNetIdentity.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(UserManager<AppUser> userManager,
                            SignInManager<AppUser> signinManager,
                            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _logger = logger;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set;}

        public string ReturnUrl { get; set; }

        public class RegisterInputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(50)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            /*
            [Required]
            [StringLength(50)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(50)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            */

            [Required]
            [DataType(DataType.Password)]
            [StringLength(24, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

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

            

            // if there are no validation errors, proceed to user registration
            if (ModelState.IsValid)
            {
                
                var user = new AppUser
                {
                    Email = Input.Email,
                    UserName = Input.Username
                };

                var registerResult = await _userManager.CreateAsync(user, Input.Password);

                if (registerResult.Succeeded)
                {
                    // user registration succeeded, so we add claims and sing in

                    //await _userManager.AddClaimAsync(user, new Claim("FirstName", Input.FirstName));
                    //await _userManager.AddClaimAsync(user, new Claim("LastName", Input.LastName));

                    _logger.LogInformation("User succesfully registered an account.");
                    return LocalRedirect(returnUrl);
                }

                _logger.LogInformation("User was not registered.");

                foreach (var error in registerResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }
            else
            {
                _logger.LogInformation("ModelState was not valid");
            }

            // if we got this far, something failed, redisplay form
            
            return Page();
           
        }
    }
}
