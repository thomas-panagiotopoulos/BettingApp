using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class SimulationStartedDomainEvent : INotification
    {
        public string SimulationId { get; }
        public SimulationStartedDomainEvent(string simulationId)
        {
            SimulationId = simulationId;
        }
    }
}
