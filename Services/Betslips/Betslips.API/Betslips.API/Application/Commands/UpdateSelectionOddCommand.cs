using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class UpdateSelectionOddCommand : IRequest<bool>
    {
        [DataMember]
        public string SelectionId { get; private set; }

        [DataMember]
        public decimal NewOdd { get; private set; }

        public UpdateSelectionOddCommand(string selectionId, decimal newOdd)
        {
            SelectionId = selectionId;
            NewOdd = newOdd;
        }
    }
}
