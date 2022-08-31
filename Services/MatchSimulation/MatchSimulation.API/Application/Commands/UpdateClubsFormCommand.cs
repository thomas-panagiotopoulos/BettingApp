using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class UpdateClubsFormCommand : IRequest<IEnumerable<Club>>
    {
        [DataMember]
        public string SimulationId { get; private set; }

        public UpdateClubsFormCommand(string simulationId)
        {
            SimulationId = simulationId;
        }
    }
}
