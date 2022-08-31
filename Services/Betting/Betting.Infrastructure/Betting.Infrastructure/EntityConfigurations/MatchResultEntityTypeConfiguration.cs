using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.EntityConfigurations
{
    public class MatchResultEntityTypeConfiguration : IEntityTypeConfiguration<MatchResult>
    {
        public void Configure(EntityTypeBuilder<MatchResult> matchResultConfiguration)
        {
            matchResultConfiguration.ToTable("matchResult", BettingContext.DEFAULT_SCHEMA);

            matchResultConfiguration.HasKey(mr => mr.Id);

            matchResultConfiguration.Property(mr => mr.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            matchResultConfiguration.Property(mr => mr.Name)
                .HasMaxLength(200)
                .IsRequired();

            matchResultConfiguration.Property(mr => mr.TypeId)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            matchResultConfiguration.Property(mr => mr.TypeName)
                .HasMaxLength(200)
                .IsRequired();

            matchResultConfiguration.Property(mr => mr.AliasName)
                .HasMaxLength(200)
                .IsRequired();


            // Seeding the table
            matchResultConfiguration.HasData(new MatchResult(1, "WinnerHomeClub", 1, "Winner", "1"));
            matchResultConfiguration.HasData(new MatchResult(2, "WinnerDraw", 1, "Winner", "X"));
            matchResultConfiguration.HasData(new MatchResult(3, "WinnerAwayClub", 1, "Winner", "2"));
            matchResultConfiguration.HasData(new MatchResult(4, "GoalsUnder", 2, "Goals", "Under"));
            matchResultConfiguration.HasData(new MatchResult(5, "GoalsOver", 2, "Goals", "Over"));
        }
    }
}
