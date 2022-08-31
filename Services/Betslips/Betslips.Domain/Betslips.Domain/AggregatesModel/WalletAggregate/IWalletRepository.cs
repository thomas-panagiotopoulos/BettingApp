using BettingApp.Services.Betslips.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Wallet Add(Wallet wallet);

        void Update(Wallet wallet);

        bool ExistsWithGamblerId(string gamblerId);

        Task<Wallet> GetByIdAsync(string walletId);

        Task<Wallet> GetByGamblerIdAsync(string gamblerId);
    }
}
