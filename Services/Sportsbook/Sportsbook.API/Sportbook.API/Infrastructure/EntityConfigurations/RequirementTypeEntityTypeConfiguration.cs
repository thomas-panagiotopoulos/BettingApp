using BettingApp.Services.Sportbook.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Infrastructure.EntityConfigurations
{
    public class RequirementTypeEntityTypeConfiguration : IEntityTypeConfiguration<RequirementType>
    {
        public void Configure(EntityTypeBuilder<RequirementType> requirementTypeConfiguration)
        {
            requirementTypeConfiguration.ToTable("requirementType");

            requirementTypeConfiguration.HasKey(rt => rt.Id);

            requirementTypeConfiguration.Property(rt => rt.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            requirementTypeConfiguration.Property(rt => rt.Name)
                .HasMaxLength(200)
                .IsRequired();

            requirementTypeConfiguration.Property(rt => rt.AliasName)
                .HasMaxLength(200)
                .IsRequired();

            // Seeding the table
            requirementTypeConfiguration.HasData(new RequirementType(1, "norequirement", "No Req"));
            requirementTypeConfiguration.HasData(new RequirementType(2, "minimumselections", "Min Sel"));
            requirementTypeConfiguration.HasData(new RequirementType(3, "minimumwageredamount", "Min Wag"));
            requirementTypeConfiguration.HasData(new RequirementType(4, "maximumselections", "Max Sel"));
            requirementTypeConfiguration.HasData(new RequirementType(5, "maximumwageredamount", "Max Wag"));
        }
    }
}
