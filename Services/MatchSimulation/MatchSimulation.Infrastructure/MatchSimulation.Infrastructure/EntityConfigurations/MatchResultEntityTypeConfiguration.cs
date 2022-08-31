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
    public class MatchResultEntityTypeConfiguration : IEntityTypeConfiguration<MatchResult>
    {
        public void Configure(EntityTypeBuilder<MatchResult> matchResultConfiguration)
        {
            matchResultConfiguration.ToTable("matchResult", MatchSimulationContext.DEFAULT_SCHEMA);

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


            // Seeding the table
            matchResultConfiguration.HasData(new MatchResult(1, "winnerhomeclub", 1, "winner"));
            matchResultConfiguration.HasData(new MatchResult(2, "winnerdraw", 1, "winner"));
            matchResultConfiguration.HasData(new MatchResult(3, "winnerawayclub", 1, "winner"));
            matchResultConfiguration.HasData(new MatchResult(4, "goalsunder", 2, "goals"));
            matchResultConfiguration.HasData(new MatchResult(5, "goalsover", 2, "goals"));
        }
    }
}
