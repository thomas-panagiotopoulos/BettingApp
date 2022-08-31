using BettingApp.WebApps.WebRazorPages.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public class IdentityParser : IIdentityParser<ApplicationUser>
    {
        public ApplicationUser Parse(IPrincipal principal)
        {
            // Pattern matching 'is' expression
            // assigns "claims" if "principal" is a "ClaimsPrincipal"
            if (principal is ClaimsPrincipal claims)
            {
                return new ApplicationUser
                {
                    Id = claims.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "",
                    FirstName = claims.Claims.FirstOrDefault(x => x.Type == "first_name")?.Value ?? "",
                    LastName = claims.Claims.FirstOrDefault(x => x.Type == "last_name")?.Value ?? "",
                    IdNumber = claims.Claims.FirstOrDefault(x => x.Type == "id_number")?.Value ?? "",
                    Email = claims.Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? "",
                };
            }
            throw new ArgumentException(message: "The principal must be a ClaimsPrincipal", paramName: nameof(principal));
        }
    }
}


