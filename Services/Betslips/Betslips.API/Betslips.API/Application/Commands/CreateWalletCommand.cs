using BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class CreateWalletCommand : IRequest<Wallet>
    {
        [DataMember]
        public string GamblerId { get; private set; }

        public CreateWalletCommand(string gamblerId)
        {
            GamblerId = gamblerId;
        }
    }
}
