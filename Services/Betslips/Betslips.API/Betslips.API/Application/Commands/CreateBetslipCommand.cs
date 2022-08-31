using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class CreateBetslipCommand : IRequest<Betslip>
    {
        [DataMember]
        public string GamblerId { get; private set; }

        public CreateBetslipCommand(string gamblerId)
        {
            GamblerId = gamblerId;
        }
    }
}
