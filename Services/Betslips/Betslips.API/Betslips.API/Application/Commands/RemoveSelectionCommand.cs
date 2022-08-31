using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class RemoveSelectionCommand : IRequest<Betslip>
    {
        [DataMember]
        public string GamblerId { get; private set; }

        [DataMember]
        public string SelectionId { get; private set; }

        public RemoveSelectionCommand(string gamblerId, string selectionId)
        {
            GamblerId = gamblerId;
            SelectionId = selectionId;
        }
    }
}
