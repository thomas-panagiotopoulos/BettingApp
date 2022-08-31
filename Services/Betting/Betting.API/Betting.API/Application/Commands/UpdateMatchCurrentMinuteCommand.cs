using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class UpdateMatchCurrentMinuteCommand : IRequest<bool>
    {
        [DataMember]
        public string MatchId { get; private set; }

        [DataMember]
        public string NewCurrentMinute { get; private set; }


        public UpdateMatchCurrentMinuteCommand(string matchId, string newCurrentMinute)
        {
            MatchId = matchId;
            NewCurrentMinute = newCurrentMinute;
        }
    }
}
