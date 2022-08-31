using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingApp.WebApps.WebRazorPages.Pages.Dashboard.Account
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IIdentityParser<ApplicationUser> _identityParser;

        public IndexModel(IIdentityParser<ApplicationUser> identityParser)
        {
            _identityParser = identityParser;
        }

        public ApplicationUser ApplicationUser { get; set; }

        public IActionResult OnGet()
        {
            ApplicationUser = _identityParser.Parse(User);

            return Page();
        }
    }
}
