using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class UnlinkClubsFromSimulationCommand : IRequest<IEnumerable<Club>>
    {
        [DataMember]
        public string SimulationId { get; private set; }
        public UnlinkClubsFromSimulationCommand(string simulationId)
        {
            SimulationId = simulationId;
        }
    }
}
