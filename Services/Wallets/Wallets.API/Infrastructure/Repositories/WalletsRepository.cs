using BettingApp.Services.Wallets.API.Model;
using BettingApp.Services.Wallets.API.Model.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Infrastructure.Repositories
{
    public class WalletsRepository : IWalletsRepository
    {
        private readonly WalletsContext _context;

        private int _pageSize = 10;

        public WalletsRepository(WalletsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public Wallet AddWallet(Wallet wallet)
        {
            var entity = _context.Wallets.Add(wallet).Entity;

            return entity;
        }

        public Wallet GetWalletByGamblerId(string gamblerId)
        {
            var wallet = _context.Wallets
                                 .Include(w => w.Transactions).ThenInclude(t => t.TransactionType)
                                 .FirstOrDefault(w => w.GamblerId.Equals(gamblerId));

            // If wallet is not found in the DbSet, search in the LocalView for newly added wallets.
            if (wallet == null)
            {
                wallet = _context.Wallets
                                 .Local
                                 .AsQueryable()
                                 .Include(w => w.Transactions).ThenInclude(t => t.TransactionType)
                                 .FirstOrDefault(w => w.GamblerId.Equals(gamblerId));
            }

            return wallet;

        }

        public Wallet GetWalletByGamblerIdWithoutTransactions(string gamblerId)
        {
            var wallet = _context.Wallets
                                 .FirstOrDefault(w => w.GamblerId.Equals(gamblerId));

            // If wallet is not found in the DbSet, search in the LocalView for newly added wallets.
            if (wallet == null)
            {
                wallet = _context.Wallets
                                 .Local
                                 .AsQueryable()
                                 .FirstOrDefault(w => w.GamblerId.Equals(gamblerId));
            }

            return wallet;
        }

        public bool WalletExistsWithGamblerId(string gamblerId)
        {
            return _context.Wallets.Any(w => w.GamblerId.Equals(gamblerId));
        }

        // page includes 10 latest transactions of Gambler's wallet
        public List<Transaction> GetTransactionsPage(string gamblerId, int pageNumber)
        {
            if (pageNumber < 1)
                return null;

            var walletId = _context.Wallets.SingleOrDefault(w => w.GamblerId.Equals(gamblerId)).Id;
            var transactionsPage = _context.Transactions
                                           .Where(t => t.WalletId.Equals(walletId))
                                           .OrderByDescending(t => t.DateTimeCreated)
                                           .Skip(pageNumber * _pageSize - _pageSize)
                                           .Take(_pageSize)
                                           .ToList();
            return transactionsPage;
        }

        public int GetTransactionsPagesCount(string gamblerId)
        {
            var transactions = _context.Wallets
                                       .Include(w => w.Transactions)
                                       .FirstOrDefault(w => w.GamblerId.Equals(gamblerId))?.Transactions;

            var totalTransactions = (transactions == null) ? 0 : transactions.Count();

            return (totalTransactions / _pageSize) + (totalTransactions % _pageSize > 0 ? 1 : 0);
        }
    }
}
