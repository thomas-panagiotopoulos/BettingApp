using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.Services.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BettingApp.Services.Identity.API.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterModel(ILogger<RegisterModel> logger,
                            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        
        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;

            if (User.Identity.IsAuthenticated)
            {
                // if a user is already signed in, redirect to return Url
                return Redirect(returnUrl);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;

            if (User.Identity.IsAuthenticated)
            {
                // if a user is already signed in, redirect to return Url
                return Redirect(returnUrl);
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    IdNumber = Input.IdNumber,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Errors.Count() > 0)
                {
                    AddErrors(result);
                    // If we got this far, something failed, redisplay form
                    return Page();
                }
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }
            else if (ModelState.IsValid)
            {
                return RedirectToPage("/Auth/Signin", new { returnUrl = returnUrl });
            }
            else
            {
                return Page();
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }

    public class RegisterInputModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "ID Number")]
        public string IdNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

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
}
