using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Infrastructure.EntityConfigurations
{
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> matchConfiguration)
        {
            matchConfiguration.ToTable("matches", MatchSimulationContext.DEFAULT_SCHEMA);

            matchConfiguration.HasKey(s => s.Id);

            matchConfiguration.Ignore(s => s.DomainEvents);

            matchConfiguration
                .Property<string>(m => m.SimulationId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SimulationId")
                .IsRequired();

            //matchConfiguration
            //    .HasOne(m => m.HomeClub)
            //    .WithMany()
            //    .HasForeignKey(m => m.HomeClubId)
            //    .OnDelete(DeleteBehavior.NoAction);

            matchConfiguration
                .Property<string>(m => m.HomeClubId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HomeClubId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.HomeClubName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HomeClubName")
                .IsRequired();

            //matchConfiguration
            //    .HasOne(m => m.AwayClub)
            //    .WithMany()
            //    .HasForeignKey(m => m.AwayClubId)
            //    .OnDelete(DeleteBehavior.NoAction);

            matchConfiguration
                .Property<string>(m => m.AwayClubId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AwayClubId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.AwayClubName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AwayClubName")
                .IsRequired();

            matchConfiguration
                .HasOne(m => m.League)
                .WithMany()
                .HasForeignKey(m => m.LeagueId)
                .OnDelete(DeleteBehavior.NoAction);

            matchConfiguration
                .Property<int>(m => m.LeagueId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("LeagueId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.LeagueName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("LeagueName")
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
                .HasMany(m => m.PossiblePicks)
                .WithOne()
                .HasForeignKey(p => p.MatchId);

            var navigation = matchConfiguration.Metadata.FindNavigation(nameof(Match.PossiblePicks));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the PossiblePick collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
