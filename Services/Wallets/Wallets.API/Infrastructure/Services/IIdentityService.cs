namespace BettingApp.Services.Wallets.API.Infrastructure.Services
{ 
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
