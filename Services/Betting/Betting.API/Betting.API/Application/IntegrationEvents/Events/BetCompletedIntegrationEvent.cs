using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.Events
{
    public class BetCompletedIntegrationEvent : IntegrationEvent
    {
        public string BetId { get; }

        public string GamblerId { get; }

        public bool IsWon { get; }

        public decimal TotalWinnings { get; }

        public BetCompletedIntegrationEvent(string betId, string gamblerId, bool isWon, decimal totalWinnings)
        {
            BetId = betId;
            GamblerId = gamblerId;
            IsWon = isWon;
            TotalWinnings = totalWinnings;  
        }
    }
}
