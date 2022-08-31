using BettingApp.Services.Wallets.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Infrastructure.EntityConfigurations
{
    public class WalletEntityTypeConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> walletConfiguration)
        {
            walletConfiguration.ToTable("wallets");

            walletConfiguration.HasKey(w => w.Id);

            walletConfiguration.HasAlternateKey(w => w.GamblerId);

            walletConfiguration.Property<string>(w => w.GamblerId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("GamblerId")
                              .IsRequired();

            walletConfiguration.Property<decimal>(w => w.Balance)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("Balance")
                              .IsRequired();

            walletConfiguration.Property<decimal>(w => w.PreviousBalance)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("PreviousBalance")
                              .IsRequired();

            walletConfiguration.Property<DateTime>(w => w.LastTimeUpdated)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("LastTimeUpdated")
                              .IsRequired();

            walletConfiguration.Property<string>(w => w.LastTransactionId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("LastTransactionId")
                              .IsRequired();

            walletConfiguration.Property<int>(w => w.TotalTransactions)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("TotalTransactions")
                              .IsRequired();

            walletConfiguration.Property<decimal>(w => w.TotalWageredAmount)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("TotalWageredAmount")
                              .IsRequired();

            walletConfiguration.Property<decimal>(w => w.TotalWinningsAmount)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("TotalWinningsAmount")
                              .IsRequired();

            walletConfiguration.Property<decimal>(w => w.TotalTopUpAmount)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("TotalTopUpAmount")
                              .IsRequired();

            walletConfiguration.Property<decimal>(w => w.TotalWithdrawAmount)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("TotalWithdrawAmount")
                              .IsRequired();

            walletConfiguration.HasMany(w => w.Transactions)
                                .WithOne()
                                .HasForeignKey(t => t.WalletId);
        }
    }
}
