namespace BettingApp.Services.Betting.API.Infrastructure.Services
{ 
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
