using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportsbook.SignalrHub.IntegrationEvents.Events
{
    public class SportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }

        public int MatchResultId { get; }
        public string MatchResultAliasName { get; }

        public decimal NewOdd { get; }

        public bool IsBetable { get; }

        public SportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent(string matchId, int matchResultId, 
                                                    string matchResultAliasName, decimal newOdd, bool isBetable)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
            MatchResultAliasName = matchResultAliasName;
            NewOdd = newOdd;
            IsBetable = isBetable;
        }
    }
}
