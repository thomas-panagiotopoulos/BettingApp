using BettingApp.Services.Wallets.API.Infrastructure.EntityConfigurations;
using BettingApp.Services.Wallets.API.Model;
using BettingApp.Services.Wallets.API.Model.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Infrastructure
{
    public class WalletsContext : DbContext, IUnitOfWork
    {
        public WalletsContext(DbContextOptions<WalletsContext> options) : base(options)
        {
        }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new WalletEntityTypeConfiguration());
            builder.ApplyConfiguration(new TransactionEntityTypeConfiguration());
            builder.ApplyConfiguration(new TransactionTypeEntityTypeConfiguration());
        }
    }
}
