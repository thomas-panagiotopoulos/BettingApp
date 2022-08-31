using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class RecalculateSelectionBetableStatusCommand : IRequest<bool>
    {
        [DataMember]
        public string SelectionId { get; private set; }

        public RecalculateSelectionBetableStatusCommand(string selectionId)
        {
            SelectionId = selectionId;
        }
    }
}
