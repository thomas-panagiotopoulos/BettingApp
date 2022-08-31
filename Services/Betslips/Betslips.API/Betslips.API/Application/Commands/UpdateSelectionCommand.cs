using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class UpdateSelectionCommand : IRequest<bool>
    {
        [DataMember]
        public string SelectionId { get; private set; }

        [DataMember]
        public decimal NewOdd { get; private set; }

        [DataMember]
        public bool IsDisabled { get; private set; }

        public UpdateSelectionCommand(string selectionId, decimal newOdd, bool isDisabled)
        {
            SelectionId = selectionId;
            NewOdd = newOdd;
            IsDisabled = isDisabled;
        }
    }
}
