using BettingApp.Services.Wallets.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Infrastructure.EntityConfigurations
{
    public class TransactionTypeEntityTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> transactionTypeConfiguration)
        {
            transactionTypeConfiguration.ToTable("transactionType");

            transactionTypeConfiguration.HasKey(tt => tt.Id);

            transactionTypeConfiguration.Ignore(tt => tt.Apply);

            transactionTypeConfiguration.Property(tt => tt.Id)
                                    .HasDefaultValue(1)
                                    .ValueGeneratedNever()
                                    .IsRequired();

            transactionTypeConfiguration.Property(tt => tt.Name)
                                    .HasMaxLength(200)
                                    .IsRequired();

            transactionTypeConfiguration.Property(tt => tt.IdentifierName)
                                    .HasMaxLength(200)
                                    .IsRequired();

            // Seeding the table
            transactionTypeConfiguration.HasData(new TransactionType(1, "TopUp", "requestId"));
            transactionTypeConfiguration.HasData(new TransactionType(2, "Withdraw", "requestId"));
            transactionTypeConfiguration.HasData(new TransactionType(3, "BetPayment", "betId"));
            transactionTypeConfiguration.HasData(new TransactionType(4, "BetWinnings", "betId"));
            transactionTypeConfiguration.HasData(new TransactionType(5, "BetRefund", "betId"));
            transactionTypeConfiguration.HasData(new TransactionType(6, "WelcomeBonus", "gamblerId"));
        }
    }
}
