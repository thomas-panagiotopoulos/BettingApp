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
    public class SimulationEntityTypeConfiguration : IEntityTypeConfiguration<Simulation>
    {
        public void Configure(EntityTypeBuilder<Simulation> simulationConfiguration)
        {
            simulationConfiguration.ToTable("simulations", MatchSimulationContext.DEFAULT_SCHEMA);

            simulationConfiguration.HasKey(s => s.Id);

            simulationConfiguration.Ignore(s => s.DomainEvents);

            //simulationConfiguration
            //    .HasOne(s => s.Match)
            //    .WithOne()
            //    .HasForeignKey<Match>(m => m.SimulationId);
            //
            //var matchNavigation = simulationConfiguration.Metadata.FindNavigation(nameof(Simulation.Match));
            //
            //// DDD Patterns comment:
            ////Set as field (New since EF 1.1) to access the Match property through its field
            //matchNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            simulationConfiguration
                .Property<string>(s => s.MatchId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("MatchId")
                .IsRequired();

            simulationConfiguration
                .Property<string>(s => s.CurrentMinute)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CurrentMinute")
                .IsRequired();

            simulationConfiguration
                .Property<int>(s => s.CurrentMinuteInt)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CurrentMinuteInt")
                .IsRequired();

            simulationConfiguration
                .Property<int>(s => s.HomeClubScore)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HomeClubScore")
                .IsRequired();

            simulationConfiguration
                .Property<int>(s => s.AwayClubScore)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AwayClubScore")
                .IsRequired();

            simulationConfiguration
                .Property<int>(s => s.GoalsScored)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("GoalsScored")
                .IsRequired();

            simulationConfiguration
                .Property<int>(s => s.RemainingExtraTimeMinutes)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RemainingExtraTimeMinutes")
                .IsRequired();

            simulationConfiguration
                .Property<int>(s => s.CurrentExtraTimeMinute)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CurrentExtraTimeMinute")
                .IsRequired();

            simulationConfiguration
                .Property<int>(s => s.MinutesPassedInHalfTime)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("MinutesPassedInHalfTime")
                .IsRequired();

            simulationConfiguration
                .HasOne(s => s.Status)
                .WithMany()
                .HasForeignKey(s => s.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            simulationConfiguration
                .Property<int>(s => s.StatusId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            simulationConfiguration
                .Property<string>(s => s.StatusName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusName")
                .IsRequired();

        }
    }
}
