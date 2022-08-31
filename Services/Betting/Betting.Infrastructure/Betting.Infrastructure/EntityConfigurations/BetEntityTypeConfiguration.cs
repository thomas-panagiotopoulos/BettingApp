using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.EntityConfigurations
{
    public class BetEntityTypeConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> betConfiguration)
        {
            betConfiguration.ToTable("bets", BettingContext.DEFAULT_SCHEMA);

            betConfiguration.HasKey(b => b.Id);

            betConfiguration.Ignore(b => b.DomainEvents);

            betConfiguration
                .Property<string>(b => b.GamblerId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("GamblerId")
                .IsRequired();

            betConfiguration
                .Property<DateTime>(b => b.DateTimeCreated)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("DateTimeCreated")
                .IsRequired();

            betConfiguration
                .Property<bool>(b => b.IsPaid)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsPaid")
                .IsRequired();

            betConfiguration
                .Property<bool>(b => b.IsCancelable)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsCancelable")
                .IsRequired();

            betConfiguration
                .HasOne(b => b.Status)
                .WithMany()
                .HasForeignKey(b => b.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
            //betConfiguration.Ignore(b => b.Status);

            betConfiguration
                .Property<int>(b => b.StatusId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            betConfiguration
                .Property<string>(b => b.StatusName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusName")
                .IsRequired();

            betConfiguration
                .HasOne(b => b.Result)
                .WithMany()
                .HasForeignKey(b => b.ResultId)
                .OnDelete(DeleteBehavior.NoAction);
            //betConfiguration.Ignore(b => b.Result);

            betConfiguration
                .Property<int>(b => b.ResultId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BetResultId")
                .IsRequired();

            betConfiguration
                .Property<string>(b => b.ResultName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BetResultName")
                .IsRequired();

            betConfiguration
                .Property<decimal>(b => b.WageredAmount)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("WageredAmount")
                .IsRequired();

            betConfiguration
                .Property<decimal>(b => b.TotalOdd)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("TotalOdd")
                .IsRequired();

            betConfiguration
                .Property<decimal>(b => b.PotentialWinnings)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PotentialWinnings")
                .IsRequired();

            betConfiguration
                .Property<decimal>(b => b.PotentialProfit)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PotentialProfit")
                .IsRequired();

            betConfiguration
                .Property<decimal>(b => b.InitialTotalOdd)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("InitialTotalOdd")
                .IsRequired();

            betConfiguration
                .Property<decimal>(b => b.InitialPotentialWinnings)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("InitialPotentialWinnings")
                .IsRequired();

            betConfiguration
                .Property<decimal>(b => b.InitialPotentialProfit)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("InitialPotentialProfit")
                .IsRequired();

            betConfiguration
                .HasMany(b => b.Selections)
                .WithOne()
                .HasForeignKey(s => s.BetId);

            var navigation = betConfiguration.Metadata.FindNavigation(nameof(Bet.Selections));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the Selection collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
