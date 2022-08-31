using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure.EntityConfigurations
{
    public class RequirementEntityTypeConfiguration : IEntityTypeConfiguration<Requirement>
    {
        public void Configure(EntityTypeBuilder<Requirement> requirementConfiguration)
        {
            requirementConfiguration.ToTable("requirements", BetslipsContext.DEFAULT_SCHEMA);

            requirementConfiguration.HasKey(r => r.Id);

            requirementConfiguration.Ignore(r => r.DomainEvents);

            requirementConfiguration
                .Property<string>(r => r.SelectionId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SelectionId")
                .IsRequired();

            requirementConfiguration
                .Property<string>(r => r.RelatedMatchId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RelatedMatchId")
                .IsRequired();

            requirementConfiguration
                .Property<int>(r => r.SelectionTypeId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SelectionTypeId")
                .IsRequired();

            requirementConfiguration
                .HasOne(r => r.RequirementType)
                .WithMany()
                .HasForeignKey(r => r.RequirementTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            requirementConfiguration
                .Property<int>(r => r.RequirementTypeId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RequirementTypeId")
                .IsRequired();

            requirementConfiguration
                .Property<string>(r => r.RequirementTypeName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RequirementTypeName")
                .IsRequired();

            requirementConfiguration
                .Property<decimal>(r => r.RequiredValue)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RequiredValue")
                .IsRequired();

            requirementConfiguration
                .Property<bool>(r => r.IsFulfilled)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("IsFulfilled")
                .IsRequired();
        }
    }
}
