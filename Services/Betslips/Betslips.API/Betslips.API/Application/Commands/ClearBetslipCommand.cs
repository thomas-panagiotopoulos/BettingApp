using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class ClearBetslipCommand : IRequest<bool>
    {
        [DataMember]
        public string GamblerId { get; private set; }

        public ClearBetslipCommand(string gamblerId)
        {
            GamblerId = gamblerId;
        }
    }
}
