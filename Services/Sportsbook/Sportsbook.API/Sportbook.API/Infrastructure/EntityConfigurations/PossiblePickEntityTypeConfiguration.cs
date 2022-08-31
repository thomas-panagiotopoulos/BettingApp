using BettingApp.Services.Sportbook.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Infrastructure.EntityConfigurations
{
    public class PossiblePickEntityTypeConfiguration : IEntityTypeConfiguration<PossiblePick>
    {
        public void Configure(EntityTypeBuilder<PossiblePick> possiblePickConfiguration)
        {
            possiblePickConfiguration.ToTable("possiblePicks");

            possiblePickConfiguration.HasKey(p => p.Id);

            possiblePickConfiguration.HasAlternateKey(p => new { p.MatchId, p.MatchResultId});

            possiblePickConfiguration.Property<string>(p => p.MatchId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("MatchId")
                              .IsRequired();

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

            possiblePickConfiguration.Property<decimal>(p => p.InitialOdd)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("InitialOdd")
                              .IsRequired();

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

            possiblePickConfiguration.Property<bool>(p => p.IsBetable)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("IsBetable")
                              .IsRequired();

            possiblePickConfiguration.HasOne(p => p.MatchResult)
                                     .WithMany()
                                     .HasForeignKey(p => p.MatchResultId)
                                     .OnDelete(DeleteBehavior.NoAction);

            possiblePickConfiguration.HasOne(p => p.RequirementType)
                                     .WithMany()
                                     .HasForeignKey(p => p.RequirementTypeId)
                                     .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
