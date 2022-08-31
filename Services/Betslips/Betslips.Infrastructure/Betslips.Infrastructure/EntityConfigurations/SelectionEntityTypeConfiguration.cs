using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using BettingApp.Services.Betslips.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure.EntityConfigurations
{
    public class SelectionEntityTypeConfiguration : IEntityTypeConfiguration<Selection>
    {
        public void Configure(EntityTypeBuilder<Selection> selectionConfiguration)
        {
            selectionConfiguration.ToTable("selections", BetslipsContext.DEFAULT_SCHEMA);

            selectionConfiguration.HasKey(s => s.Id);

            selectionConfiguration.Ignore(s => s.DomainEvents);

            selectionConfiguration
                .Property<string>(s => s.BetslipId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BetslipId")
                .IsRequired();

            selectionConfiguration
                .Property<decimal>(s => s.Odd)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Odd")
                .IsRequired();

            selectionConfiguration
                .Property<decimal>(s => s.InitialOdd)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("InitialOdd")
                .IsRequired();

            selectionConfiguration
                .HasOne(s => s.GamblerMatchResult)
                .WithMany()
                .HasForeignKey(s => s.GamblerMatchResultId)
                .OnDelete(DeleteBehavior.NoAction);

            selectionConfiguration
                .Property<int>(s => s.GamblerMatchResultId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("GamblerMatchResultId")
                .IsRequired();

            selectionConfiguration
                .Property<string>(s => s.GamblerMatchResultName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("GamblerMatchResultName")
                .IsRequired();

            selectionConfiguration
                .Property<int>(s => s.SelectionTypeId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SelectionTypeId")
                .IsRequired();

            selectionConfiguration
                .Property<string>(s => s.SelectionTypeName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SelectionTypeName")
                .IsRequired();

            selectionConfiguration
                .Property<bool>(s => s.IsCanceled)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsCanceled")
                .IsRequired();

            selectionConfiguration
                .Property<bool>(s => s.IsDisabled)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsDisabled")
                .IsRequired();

            selectionConfiguration
                .Property<bool>(s => s.IsBetable)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsBetable")
                .IsRequired();

            selectionConfiguration
                .HasOne(s => s.Match)
                .WithOne()
                .HasForeignKey<Match>(m => m.SelectionId);

            var matchNavigation = selectionConfiguration.Metadata.FindNavigation(nameof(Selection.Match));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the Match property through its field
            matchNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            selectionConfiguration
                .HasOne(s => s.Requirement)
                .WithOne()
                .HasForeignKey<Requirement>(r => r.SelectionId);

            var requirementNavigation = selectionConfiguration.Metadata.FindNavigation(nameof(Selection.Requirement));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the Requirement property through its field
            requirementNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);


        }
    }
}
