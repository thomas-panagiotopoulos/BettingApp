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
    public class RequirementTypeEntityTypeConfiguration : IEntityTypeConfiguration<RequirementType>
    {
        public void Configure(EntityTypeBuilder<RequirementType> requirementTypeConfiguration)
        {
            requirementTypeConfiguration.ToTable("requirementType", MatchSimulationContext.DEFAULT_SCHEMA);

            requirementTypeConfiguration.HasKey(rt => rt.Id);

            requirementTypeConfiguration.Property(rt => rt.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            requirementTypeConfiguration.Property(rt => rt.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Seeding the table
            requirementTypeConfiguration.HasData(new RequirementType(1, "norequirement"));
            requirementTypeConfiguration.HasData(new RequirementType(2, "minimumselections"));
            requirementTypeConfiguration.HasData(new RequirementType(3, "minimumwageredamount"));
            requirementTypeConfiguration.HasData(new RequirementType(4, "maximumselections"));
            requirementTypeConfiguration.HasData(new RequirementType(5, "maximumwageredamount"));
        }
    }
}
