using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class CancelMatchCommand : IRequest<bool>
    {
        [DataMember]
        public string MatchId { get; private set; }

        public CancelMatchCommand(string matchId)
        {
            MatchId = matchId;
        }
    }
}
