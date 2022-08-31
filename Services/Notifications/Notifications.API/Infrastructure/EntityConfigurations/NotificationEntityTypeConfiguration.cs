using BettingApp.Services.Notifications.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Infrastructure.EntityConfigurations
{
    public class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> notificationConfiguration)
        {
            notificationConfiguration.ToTable("notifications");

            notificationConfiguration.HasKey(n => n.Id);

            notificationConfiguration.Property<string>(n => n.GamblerId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("GamblerId")
                              .IsRequired();

            notificationConfiguration.Property<string>(n => n.GamblerId)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("GamblerId")
                              .IsRequired();

            notificationConfiguration.Property<DateTime>(n => n.DateTimeCreated)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("DateTimeCreated")
                              .IsRequired();

            notificationConfiguration.Property<string>(n => n.Title)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("Title")
                              .IsRequired();

            notificationConfiguration.Property<string>(n => n.Description)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("Description")
                              .IsRequired();

            notificationConfiguration.Property<bool>(n => n.IsRead)
                              .UsePropertyAccessMode(PropertyAccessMode.Field)
                              .HasColumnName("IsRead")
                              .IsRequired();
        }
    }
}
