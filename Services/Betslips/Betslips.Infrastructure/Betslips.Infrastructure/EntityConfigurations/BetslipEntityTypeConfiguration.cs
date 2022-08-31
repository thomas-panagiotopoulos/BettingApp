using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure.EntityConfigurations
{
    public class BetslipEntityTypeConfiguration : IEntityTypeConfiguration<Betslip>
    {
        public void Configure(EntityTypeBuilder<Betslip> betslipConfiguration)
        {
            betslipConfiguration.ToTable("betslips", BetslipsContext.DEFAULT_SCHEMA);

            betslipConfiguration.HasKey(b => b.Id);

            betslipConfiguration.HasAlternateKey(b => b.GamblerId);

            betslipConfiguration.Ignore(b => b.DomainEvents);

            betslipConfiguration
                .Property<decimal>(b => b.WageredAmount)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("WageredAmount")
                .IsRequired();

            betslipConfiguration
                .Property<decimal>(b => b.TotalOdd)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("TotalOdd")
                .IsRequired();

            betslipConfiguration
                .Property<decimal>(b => b.PotentialWinnings)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PotentialWinnings")
                .IsRequired();

            betslipConfiguration
                .Property<decimal>(b => b.PotentialProfit)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PotentialProfit")
                .IsRequired();

            betslipConfiguration
                .Property<bool>(b => b.IsBetable)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsBetable")
                .IsRequired();

            betslipConfiguration
                .Property<string>(b => b.LatestAdditionId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("LatestAdditionId")
                .IsRequired();

            betslipConfiguration
                .HasMany(b => b.Selections)
                .WithOne()
                .HasForeignKey(s => s.BetslipId);

            var navigation = betslipConfiguration.Metadata.FindNavigation(nameof(Betslip.Selections));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the Selection collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
