using System.Security.Principal;

namespace BettingApp.WebApps.WebRazorPages.Services
{ 
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
