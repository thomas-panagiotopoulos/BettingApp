using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.EntityConfigurations
{
    public class StatusEntityTypeConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> statusConfiguration)
        {
            statusConfiguration.ToTable("status", BettingContext.DEFAULT_SCHEMA);

            statusConfiguration.HasKey(s => s.Id);

            statusConfiguration.Property(s => s.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            statusConfiguration.Property(s => s.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Seeding the table
            statusConfiguration.HasData(new Status(1,"Pending"));
            statusConfiguration.HasData(new Status(2, "Ongoing"));
            statusConfiguration.HasData(new Status(3, "Completed"));
            statusConfiguration.HasData(new Status(4, "Canceled"));

        }
    }
}
