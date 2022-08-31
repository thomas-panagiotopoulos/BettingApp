using BettingApp.Services.Sportbook.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Infrastructure.EntityConfigurations
{
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> matchConfiguration)
        {
            matchConfiguration.ToTable("matches");

            matchConfiguration.HasKey(m => m.Id);

            matchConfiguration.Property<string>(m => m.HomeClubName)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("HomeClubName")
                              .IsRequired();

            matchConfiguration.Property<string>(m => m.AwayClubName)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("AwayClubName")
                              .IsRequired();

            matchConfiguration.HasOne(m => m.League)
                              .WithMany()
                              .HasForeignKey(m => m.LeagueId);

            matchConfiguration.Property<int>(m => m.LeagueId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("LeagueId")
                              .IsRequired();

            matchConfiguration.Property<string>(m => m.LeagueName)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("LeagueName")
                              .IsRequired();

            matchConfiguration.Property<DateTime>(m => m.KickoffDateTime)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("KickoffDateTime")
                              .IsRequired();

            matchConfiguration.Property<string>(m => m.CurrentMinute)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("CurrentMinute")
                              .IsRequired();

            matchConfiguration.Property<int>(m => m.HomeClubScore)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("HomeClubScore")
                              .IsRequired();

            matchConfiguration.Property<int>(m => m.AwayClubScore)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("AwayClubScore")
                              .IsRequired();

            matchConfiguration.Property<bool>(m => m.IsCanceled)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("IsCanceled")
                              .IsRequired();

            matchConfiguration.Property<bool>(m => m.IsBetable)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("IsBetable")
                              .IsRequired();

            matchConfiguration.HasMany(m => m.PossiblePicks)
                              .WithOne()
                              .HasForeignKey(p => p.MatchId);

        }
    }
}
