using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class RecalculateBetStatusCommand : IRequest<bool>
    {
        [DataMember]
        public string BetId { get; private set; }

        public RecalculateBetStatusCommand(string betId)
        {
            BetId = betId;
        }
    }
}
