using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class CancelSimulationCommand : IRequest<Simulation>
    {
        [DataMember]
        public string SimulationId { get; private set; }
        public CancelSimulationCommand(string simulationId)
        {
            SimulationId = simulationId;
        }
    }
}
