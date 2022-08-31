using BettingApp.Services.Wallets.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Model
{
    public interface IWalletsRepository : IRepository
    {
        Wallet AddWallet(Wallet wallet);

        Wallet GetWalletByGamblerId(string gamblerId);

        Wallet GetWalletByGamblerIdWithoutTransactions(string gamblerId);

        bool WalletExistsWithGamblerId(string gamblerId);

        List<Transaction> GetTransactionsPage(string gamblerId, int pageNumber);

        int GetTransactionsPagesCount(string gamblerId);
    }
}
