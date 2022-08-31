using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.EntityConfigurations
{
    public class SelectionEntityTypeConfiguration : IEntityTypeConfiguration<Selection>
    {
        public void Configure(EntityTypeBuilder<Selection> selectionConfiguration)
        {
            selectionConfiguration.ToTable("selections", BettingContext.DEFAULT_SCHEMA);

            selectionConfiguration.HasKey(s => s.Id);

            selectionConfiguration.Ignore(s => s.DomainEvents);

            selectionConfiguration
                .Property<string>(s => s.BetId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BetId")
                .IsRequired();

            selectionConfiguration
                .Property<int>(s => s.StatusId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            selectionConfiguration
                .Property<string>(s => s.StatusName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusName")
                .IsRequired();

            selectionConfiguration.HasOne(s => s.Status)
                                  .WithMany()
                                  .HasForeignKey(s => s.StatusId)
                                  .OnDelete(DeleteBehavior.NoAction);
            //selectionConfiguration.Ignore(s => s.Status);


            selectionConfiguration
                .Property<int>(s => s.ResultId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ResultId")
                .IsRequired();

            selectionConfiguration
                .Property<string>(s => s.ResultName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ResultName")
                .IsRequired();

            selectionConfiguration.HasOne(s => s.Result)
                                  .WithMany()
                                  .HasForeignKey(s => s.ResultId)
                                  .OnDelete(DeleteBehavior.NoAction);
            //selectionConfiguration.Ignore(s => s.Result);

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

            selectionConfiguration.HasOne(s => s.GamblerMatchResult)
                                  .WithMany()
                                  .HasForeignKey(s => s.GamblerMatchResultId)
                                  .OnDelete(DeleteBehavior.NoAction);
            //selectionConfiguration.Ignore(s => s.GamblerMatchResult);

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
                .Property<decimal>(s => s.Odd)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Odd")
                .IsRequired();

            selectionConfiguration
                .HasOne(s => s.Match)
                .WithOne()
                .HasForeignKey<Match>(m => m.SelectionId);

            var navigation = selectionConfiguration.Metadata.FindNavigation(nameof(Selection.Match));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the Match property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);


        }
    }
}
