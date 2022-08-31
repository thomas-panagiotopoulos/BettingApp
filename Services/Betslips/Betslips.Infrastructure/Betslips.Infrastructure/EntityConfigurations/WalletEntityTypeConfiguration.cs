using BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure.EntityConfigurations
{
    public class WalletEntityTypeConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> walletConfiguration)
        {
            walletConfiguration.ToTable("wallets", BetslipsContext.DEFAULT_SCHEMA);

            walletConfiguration.HasKey(w => w.Id);

            walletConfiguration.Ignore(w => w.DomainEvents);

            walletConfiguration
                .Property<string>(w => w.GamblerId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("GamblerId")
                .IsRequired();

            walletConfiguration
                .Property<decimal>(w => w.Balance)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Balance")
                .IsRequired();

            walletConfiguration
                .Property<decimal>(w => w.PreviousBalance)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PreviousBalance")
                .IsRequired();

            walletConfiguration
                .Property<DateTime>(w => w.LastTimeUpdated)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("LastTimeUpdated")
                .IsRequired();
        }
    }
}
