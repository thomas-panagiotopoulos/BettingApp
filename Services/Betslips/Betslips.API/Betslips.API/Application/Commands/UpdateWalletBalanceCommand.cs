using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class UpdateWalletBalanceCommand : IRequest<bool>
    {
        [DataMember]
        public string GamblerId { get; private set; }

        [DataMember]
        public decimal NewBalance { get; private set; }

        [DataMember]
        public decimal OldBalance { get; private set; }

        public UpdateWalletBalanceCommand(string gamblerId, decimal newBalance, decimal oldBalance)
        {
            GamblerId = gamblerId;
            NewBalance = newBalance;
            OldBalance = oldBalance;
        }
    }
}
