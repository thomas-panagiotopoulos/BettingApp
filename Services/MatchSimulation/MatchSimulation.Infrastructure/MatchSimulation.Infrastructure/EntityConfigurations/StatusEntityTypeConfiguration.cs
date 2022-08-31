using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Infrastructure.EntityConfigurations
{
    public class StatusEntityTypeConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> statusConfiguration)
        {
            statusConfiguration.ToTable("status", MatchSimulationContext.DEFAULT_SCHEMA);

            statusConfiguration.HasKey(s => s.Id);

            statusConfiguration.Property(s => s.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            statusConfiguration.Property(s => s.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Seeding the table
            statusConfiguration.HasData(new Status(1, "pending"));
            statusConfiguration.HasData(new Status(2, "ongoing"));
            statusConfiguration.HasData(new Status(3, "completed"));
            statusConfiguration.HasData(new Status(4, "canceled"));
        }
    }
}
