namespace BettingApp.Services.Betslips.API.Infrastructure.Services
{ 
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
