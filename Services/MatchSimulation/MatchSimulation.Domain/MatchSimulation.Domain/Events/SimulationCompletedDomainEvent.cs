using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class SimulationCompletedDomainEvent : INotification
    {
        public string SimulationId { get; }
        public string MatchId { get; }
        public SimulationCompletedDomainEvent(string simulationId, string matchId)
        {
            SimulationId = simulationId;
            MatchId = matchId;
        }
    }
}
