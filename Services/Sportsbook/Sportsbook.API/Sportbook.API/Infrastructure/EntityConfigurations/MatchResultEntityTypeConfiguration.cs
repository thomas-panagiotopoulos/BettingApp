using BettingApp.Services.Sportbook.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Infrastructure.EntityConfigurations
{
    public class MatchResultEntityTypeConfiguration : IEntityTypeConfiguration<MatchResult>
    {
        public void Configure(EntityTypeBuilder<MatchResult> matchResultConfiguration)
        {
            matchResultConfiguration.ToTable("matchResult");

            matchResultConfiguration.HasKey(mr => mr.Id);

            matchResultConfiguration.Property(mr => mr.Id)
                                    .HasDefaultValue(1)
                                    .ValueGeneratedNever()
                                    .IsRequired();

            matchResultConfiguration.Property(mr => mr.Name)
                                    .HasMaxLength(200)
                                    .IsRequired();

            matchResultConfiguration.Property(mr => mr.AliasName)
                                    .HasMaxLength(200)
                                    .IsRequired();

            // Seeding the table
            matchResultConfiguration.HasData(new MatchResult(1, "winnerhomeclub", "1"));
            matchResultConfiguration.HasData(new MatchResult(2, "winnerdraw", "X"));
            matchResultConfiguration.HasData(new MatchResult(3, "winnerawayclub", "2"));
            matchResultConfiguration.HasData(new MatchResult(4, "goalsunder", "Under"));
            matchResultConfiguration.HasData(new MatchResult(5, "goalsover", "Over"));
        }
    }
}
