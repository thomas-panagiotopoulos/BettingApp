using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Infrastructure.EntityConfigurations
{
    public class LeagueEntityTypeConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> leagueConfiguration)
        {
            leagueConfiguration.ToTable("league", MatchSimulationContext.DEFAULT_SCHEMA);

            leagueConfiguration.Ignore(l => l.MatchKickoffDaysAndTimes);

            leagueConfiguration.HasKey(l => l.Id);

            leagueConfiguration.Property(l => l.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            leagueConfiguration.Property(l => l.Name)
                .HasMaxLength(200)
                .IsRequired();

            leagueConfiguration.Property(l => l.TypeId)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            leagueConfiguration.Property(l => l.TypeName)
                .HasMaxLength(200)
                .IsRequired();


            // Seeding the table
            leagueConfiguration.HasData(new League(1, "GreekSuperLeague", 1, "domestic"));
            leagueConfiguration.HasData(new League(2, "EnglishPremierLeague", 1, "domestic"));
            leagueConfiguration.HasData(new League(3, "SpanishLaLiga", 1, "domestic"));
            leagueConfiguration.HasData(new League(4, "ItalianSerieA", 1, "domestic"));
            leagueConfiguration.HasData(new League(5, "GermanBundesliga", 1, "domestic"));
            leagueConfiguration.HasData(new League(6, "NoDomesticLeague", 1, "domestic"));
            leagueConfiguration.HasData(new League(7, "ChampionsLeague", 2, "continental"));
            leagueConfiguration.HasData(new League(8, "EuropaLeague", 2, "continental"));
            leagueConfiguration.HasData(new League(9, "EuropaConferenceLeague", 2, "continental"));
            leagueConfiguration.HasData(new League(10, "NoContinentalLeague", 2, "continental"));
        }
    }
}
