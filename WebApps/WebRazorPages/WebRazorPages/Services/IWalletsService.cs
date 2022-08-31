using BettingApp.WebApps.WebRazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public interface IWalletsService
    {
        Task<WalletPreview> GetWalletPreview();
        Task<IEnumerable<Transaction>> GetTransactionsPage(int pageNumber);
        Task<int> GetTransactionsPagesCount();
        Task<string> RequestTopUp(decimal topUpAmount, Card card);
        Task<string> RequestWithdraw(decimal withdrawAmount, string iban, string bankAccountHolder);
        Task<bool> TopUpRequestExists(string requestId);
        Task<bool> WithdrawRequestExists(string requestId);
    }
}
