using BettingApp.Services.Sportbook.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Infrastructure.EntityConfigurations
{
    public class LeagueEntityTypeConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> leagueConfiguration)
        {

            leagueConfiguration.ToTable("league");


            leagueConfiguration.HasKey(l => l.Id);

            leagueConfiguration.Property(l => l.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            leagueConfiguration.Property(l => l.Name)
                .HasMaxLength(200)
                .IsRequired();


            // Seeding the table
            leagueConfiguration.HasData(new League(1, "GreekSuperLeague"));
            leagueConfiguration.HasData(new League(2, "EnglishPremierLeague"));
            leagueConfiguration.HasData(new League(3, "SpanishLaLiga"));
            leagueConfiguration.HasData(new League(4, "ItalianSerieA"));
            leagueConfiguration.HasData(new League(5, "GermanBundesliga"));
            leagueConfiguration.HasData(new League(6, "NoDomesticLeague"));
            leagueConfiguration.HasData(new League(7, "ChampionsLeague"));
            leagueConfiguration.HasData(new League(8, "EuropaLeague"));
            leagueConfiguration.HasData(new League(9, "EuropaConferenceLeague"));
            leagueConfiguration.HasData(new League(10, "NoContinentalLeague"));
        }
    }
}
