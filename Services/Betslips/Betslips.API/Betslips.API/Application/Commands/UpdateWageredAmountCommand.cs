using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class UpdateWageredAmountCommand : IRequest<Betslip>
    {
        [DataMember]
        public string GamblerId { get; private set; }

        [DataMember]
        public decimal NewWageredAmount { get; private set; }

        public UpdateWageredAmountCommand(string gamblerId, decimal newWageredAmount)
        {
            GamblerId = gamblerId;
            NewWageredAmount = newWageredAmount;
        }
    }
}
