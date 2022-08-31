using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class RecalculateSelectionResultCommand : IRequest<bool>
    {
        [DataMember]
        public string SelectionId { get; private set; }

        public RecalculateSelectionResultCommand(string selectionId)
        {
            SelectionId = selectionId;
        }
    }
}
