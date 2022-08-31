using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class RecalculateSelectionStatusCommand : IRequest<bool>
    {
        [DataMember]
        public string SelectionId { get; private set; }

        public RecalculateSelectionStatusCommand(string selectionId)
        {
            SelectionId = selectionId;
        }
    }
}
