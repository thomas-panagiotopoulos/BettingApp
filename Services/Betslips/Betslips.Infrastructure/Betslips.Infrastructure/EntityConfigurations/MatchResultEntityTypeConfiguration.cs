using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure.EntityConfigurations
{
    public class MatchResultEntityTypeConfiguration : IEntityTypeConfiguration<MatchResult>
    {
        public void Configure(EntityTypeBuilder<MatchResult> matchResultConfiguration)
        {
            matchResultConfiguration.ToTable("matchResult", BetslipsContext.DEFAULT_SCHEMA);

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
            matchResultConfiguration.HasData(new MatchResult(1, "winnerhomeclub", 1, "winner", "1"));
            matchResultConfiguration.HasData(new MatchResult(2, "winnerdraw", 1, "winner", "X"));
            matchResultConfiguration.HasData(new MatchResult(3, "winnerawayclub", 1, "winner", "2"));
            matchResultConfiguration.HasData(new MatchResult(4, "goalsunder", 2, "goals", "Under"));
            matchResultConfiguration.HasData(new MatchResult(5, "goalsover", 2, "goals", "Over"));
        }
    }
}
