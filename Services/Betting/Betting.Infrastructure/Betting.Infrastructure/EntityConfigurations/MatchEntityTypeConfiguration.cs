using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.EntityConfigurations
{
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> matchConfiguration)
        {
            matchConfiguration.ToTable("matches", BettingContext.DEFAULT_SCHEMA);

            matchConfiguration.HasKey(m => m.Id);

            matchConfiguration.Ignore(m => m.DomainEvents);

            matchConfiguration
                .Property<string>(m => m.SelectionId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SelectionId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.RelatedMatchId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RelatedMatchId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.HomeClubName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HomeClubName")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.AwayClubName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AwayClubName")
                .IsRequired();

            matchConfiguration
                .Property<DateTime>(m => m.KickoffDateTime)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("KincoffDateTime")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.CurrentMinute)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CurrentMinute")
                .IsRequired();

            matchConfiguration
                .Property<int>(m => m.HomeClubScore)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HomeClubScore")
                .IsRequired();

            matchConfiguration
                .Property<int>(m => m.AwayClubScore)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AwayClubScore")
                .IsRequired();


            matchConfiguration
                .Property<int>(m => m.StatusId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.StatusName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusName")
                .IsRequired();

            matchConfiguration.HasOne(m => m.Status)
                                  .WithMany()
                                  .HasForeignKey(m => m.StatusId)
                                  .OnDelete(DeleteBehavior.NoAction);
            //matchConfiguration.Ignore(m => m.Status);

            matchConfiguration
                .Property<int>(m => m.WinnerResultId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("WinnerResultId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.WinnerResultName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("WinnerResultName")
                .IsRequired();

            matchConfiguration
                .Property<int>(m => m.GoalsResultId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("GoalsResultId")
                .IsRequired();

            matchConfiguration
                .Property<string>(m => m.GoalsResultName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("GoalsResultName")
                .IsRequired();

            // Only one of "_winnerResultId" and "_goalsResultId" will be set as ForeignKey
            // for the Match.Result property. To set them both as Foreign Keys, we must create
            // two separate Result properties on Match model, e.g. WinnerResult and GoalsResult

            matchConfiguration.HasOne(m => m.Result)
                              .WithMany()
                              .HasForeignKey(m => m.WinnerResultId)
                              .OnDelete(DeleteBehavior.NoAction);

            matchConfiguration.HasOne(m => m.Result)
                              .WithMany()
                              .HasForeignKey(m => m.GoalsResultId)
                              .OnDelete(DeleteBehavior.NoAction);
            //matchConfiguration.Ignore(m => m.Result);
        }
    }
}
