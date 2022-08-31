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
    public class PossiblePickEntityTypeConfiguration : IEntityTypeConfiguration<PossiblePick>
    {
        public void Configure(EntityTypeBuilder<PossiblePick> possiblePickConfiguration)
        {
            possiblePickConfiguration.ToTable("possiblePicks", MatchSimulationContext.DEFAULT_SCHEMA);

            possiblePickConfiguration.HasKey(p => p.Id);

            possiblePickConfiguration.HasAlternateKey(p => new { p.MatchId, p.MatchResultId });

            possiblePickConfiguration.Property<string>(p => p.MatchId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("MatchId")
                              .IsRequired();

            possiblePickConfiguration.HasOne(p => p.MatchResult)
                                     .WithMany()
                                     .HasForeignKey(p => p.MatchResultId)
                                     .OnDelete(DeleteBehavior.NoAction);

            possiblePickConfiguration.Property<int>(p => p.MatchResultId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("MatchResultId")
                              .IsRequired();

            possiblePickConfiguration.Property<string>(p => p.MatchResultName)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("MatchResultName")
                              .IsRequired();

            possiblePickConfiguration.Property<decimal>(p => p.Odd)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("Odd")
                              .IsRequired();

            possiblePickConfiguration.HasOne(p => p.RequirementType)
                                     .WithMany()
                                     .HasForeignKey(p => p.RequirementTypeId)
                                     .OnDelete(DeleteBehavior.NoAction);

            possiblePickConfiguration.Property<int>(p => p.RequirementTypeId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("RequirementTypeId")
                              .IsRequired();

            possiblePickConfiguration.Property<string>(p => p.RequirementTypeName)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("RequirementTypeName")
                              .IsRequired();

            possiblePickConfiguration.Property<decimal>(p => p.RequiredValue)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("RequiredValue")
                              .IsRequired();

            possiblePickConfiguration.Property<bool>(p => p.IsCanceled)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("IsCanceled")
                              .IsRequired();

            possiblePickConfiguration.Property<bool>(p => p.IsDisabled)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("IsDisabled")
                              .IsRequired();

        }
    }
}
