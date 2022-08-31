using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure.EntityConfigurations
{
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> matchConfiguration)
        {
            matchConfiguration.ToTable("matches", BetslipsContext.DEFAULT_SCHEMA);

            matchConfiguration.HasKey(m => m.Id);

            matchConfiguration.Ignore(m => m.DomainEvents);

            matchConfiguration
                .Property<string>(m => m.SelectionId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SelectionId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.RelatedMatchId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RelatedMatchId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.HomeClubName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HomeClubName")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.AwayClubName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AwayClubName")
                .IsRequired();

            matchConfiguration
                .Property<DateTime>(m => m.KickoffDateTime)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("KickoffDateTime")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.CurrentMinute)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CurrentMinute")
                .IsRequired();

            matchConfiguration
                .Property<int>(m => m.HomeClubScore)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HomeClubScore")
                .IsRequired();

            matchConfiguration
                .Property<int>(m => m.AwayClubScore)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AwayClubScore")
                .IsRequired();

            matchConfiguration
                .Property<bool>(m => m.IsCanceled)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsCanceled")
                .IsRequired();

            matchConfiguration
                .Property<bool>(m => m.IsBetable)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsBetable")
                .IsRequired();
        }
    }
}
