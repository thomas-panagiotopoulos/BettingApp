using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BettingApp.Services.Identity.API.Services
{
    public interface ILoginService<T>
    {
        Task<bool> ValidateCredentials(T user, string password);

        Task<T> FindByUsername(string user);

        Task SignIn(T user);

        Task SignInAsync(T user, AuthenticationProperties properties, string authenticationMethod = null);

        bool IsSignedIn(ClaimsPrincipal user);
    }
}
