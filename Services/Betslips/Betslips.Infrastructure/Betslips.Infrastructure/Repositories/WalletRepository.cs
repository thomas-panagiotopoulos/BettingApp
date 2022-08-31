using BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate;
using BettingApp.Services.Betslips.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly BetslipsContext _context;

        public WalletRepository(BetslipsContext context)
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

        public Wallet Add(Wallet wallet)
        {
            var entity = _context.Wallets.Add(wallet).Entity;

            return entity;
        }

        public void Update(Wallet wallet)
        {
            _context.Entry(wallet).State = EntityState.Modified;
        }

        public bool ExistsWithGamblerId(string gamblerId)
        {
            return _context.Wallets.Any(w => w.GamblerId.Equals(gamblerId));
        }

        public async Task<Wallet> GetByIdAsync(string walletId)
        {
            var wallet = await _context.Wallets
                                       .FirstOrDefaultAsync(w => w.Id.Equals(walletId));

            // If wallet is not found in the DbSet, search in the LocalView for newly added wallets.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (wallet == null)
            {
                wallet = _context.Wallets
                                    .Local
                                    .AsQueryable()
                                    .FirstOrDefault(w => w.Id.Equals(walletId));
            }

            return wallet;
        }

        public async Task<Wallet> GetByGamblerIdAsync(string gamblerId)
        {
            var wallet = await _context.Wallets
                                       .FirstOrDefaultAsync(w => w.GamblerId.Equals(gamblerId));

            // If wallet is not found in the DbSet, search in the LocalView for newly added wallets.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (wallet == null)
            {
                wallet = _context.Wallets
                                 .Local
                                 .AsQueryable()
                                 .FirstOrDefault(w => w.GamblerId.Equals(gamblerId));
            }

            return wallet;
        }

    }
}
