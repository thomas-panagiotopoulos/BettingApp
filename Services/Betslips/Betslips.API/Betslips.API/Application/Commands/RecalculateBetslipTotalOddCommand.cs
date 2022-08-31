using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class RecalculateBetslipTotalOddCommand : IRequest<bool>
    {
        [DataMember]
        public string BetslipId { get; private set; }

        public RecalculateBetslipTotalOddCommand(string betslipId)
        {
            BetslipId = betslipId;
        }
    }
}
