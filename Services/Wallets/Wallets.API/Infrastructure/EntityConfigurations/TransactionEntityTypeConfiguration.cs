using BettingApp.Services.Wallets.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Infrastructure.EntityConfigurations
{
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> transactionConfiguration)
        {
            transactionConfiguration.ToTable("transactions");

            transactionConfiguration.HasKey(t => t.Id);

            transactionConfiguration.Property<string>(t => t.WalletId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("WalletId")
                              .IsRequired();

            transactionConfiguration.Property<decimal>(t => t.Amount)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("Amount")
                              .IsRequired();

            transactionConfiguration.Property<DateTime>(t => t.DateTimeCreated)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("DateTimeCreated")
                              .IsRequired();

            transactionConfiguration.Property<decimal>(t => t.WalletBalanceBefore)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("WalletBalanceBefore")
                              .IsRequired();

            transactionConfiguration.Property<decimal>(t => t.WalletBalanceAfter)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("WalletBalanceAfter")
                              .IsRequired();

            transactionConfiguration.Property<int>(t => t.TransactionTypeId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("TransactionTypeId")
                              .IsRequired();

            transactionConfiguration.Property<string>(t => t.TransactionTypeName)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("TransactionTypeName")
                              .IsRequired();

            transactionConfiguration.Property<string>(t => t.IdentifierId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("IdentifierId")
                              .IsRequired();

            transactionConfiguration.Property<string>(t => t.IdentifierName)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("IdentifierName")
                              .IsRequired();

            transactionConfiguration.HasOne(t => t.TransactionType)
                                    .WithMany()
                                    .HasForeignKey(t => t.TransactionTypeId);
        }
    }
}
