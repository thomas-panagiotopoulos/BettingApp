using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class SimulationCanceledDomainEvent : INotification
    {
        public string SimulationId { get; }
        public string MatchId { get; }
        public bool SimulationWasOngoing { get; }

        public SimulationCanceledDomainEvent(string simulationId, string matchId, bool simulationWasOngoing)
        {
            SimulationId = simulationId;
            MatchId = matchId;
            SimulationWasOngoing = simulationWasOngoing;
        }
    }
}
