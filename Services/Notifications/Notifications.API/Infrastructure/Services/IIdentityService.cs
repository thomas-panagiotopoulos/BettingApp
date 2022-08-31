namespace BettingApp.Services.Notifications.API.Infrastructure.Services
{ 
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
