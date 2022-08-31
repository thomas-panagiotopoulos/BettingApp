using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.EntityConfigurations
{
    public class BettingResultEntityTypeConfiguration : IEntityTypeConfiguration<BettingResult>
    {
        public void Configure(EntityTypeBuilder<BettingResult> bettingResultConfiguration)
        {
            bettingResultConfiguration.ToTable("bettingResult", BettingContext.DEFAULT_SCHEMA);

            bettingResultConfiguration.HasKey(br => br.Id);

            bettingResultConfiguration.Property(br => br.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            bettingResultConfiguration.Property(br => br.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Seeding the table
            bettingResultConfiguration.HasData(new BettingResult(1,"Won"));
            bettingResultConfiguration.HasData(new BettingResult(2, "Lost"));

        }
    }
}
