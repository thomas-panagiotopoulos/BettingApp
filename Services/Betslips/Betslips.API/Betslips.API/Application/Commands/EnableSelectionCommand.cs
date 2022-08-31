using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class EnableSelectionCommand : IRequest<bool>
    {
        [DataMember]
        public string SelectionId { get; private set; }

        public EnableSelectionCommand(string selectionId)
        {
            SelectionId = selectionId;
        }
    }
}
