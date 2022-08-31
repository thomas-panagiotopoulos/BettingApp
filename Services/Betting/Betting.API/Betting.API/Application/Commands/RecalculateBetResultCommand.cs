using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class RecalculateBetResultCommand : IRequest<bool>
    {
        [DataMember]
        public string BetId { get; private set; }

        public RecalculateBetResultCommand(string betId)
        {
            BetId = betId;
        }
    }
}
