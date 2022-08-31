using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.SignalrHub
{
    public class BetslipsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "";
            var userName = Context.User.Identity.Name;

            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var userId = Context.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "";
            var userName = Context.User.Identity.Name;

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}
