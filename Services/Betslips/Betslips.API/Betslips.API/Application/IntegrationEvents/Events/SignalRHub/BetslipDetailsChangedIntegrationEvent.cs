using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events.SignalRHub
{
    public class BetslipDetailsChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetslipId { get; }
        public decimal NewTotalOdd { get; }
        public decimal NewPotentialWinnings { get; }
        public decimal NewPotentialProfit { get; }
        public decimal NewWageredAmount { get; }

        public BetslipDetailsChangedIntegrationEvent(string gamblerId, string betslipId, 
                  decimal newtotalOdd, decimal newPotentialWinnings, decimal newPotentialProfit, decimal newWageredAmount)
        {
            GamblerId = gamblerId;
            BetslipId = betslipId;
            NewTotalOdd = newtotalOdd;
            NewPotentialWinnings = newPotentialWinnings;
            NewPotentialProfit = newPotentialProfit;
            NewWageredAmount = newWageredAmount;
        }
    }
}
