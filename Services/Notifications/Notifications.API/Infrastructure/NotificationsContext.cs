using BettingApp.Services.Notifications.API.Infrastructure.EntityConfigurations;
using BettingApp.Services.Notifications.API.Model;
using BettingApp.Services.Notifications.API.Model.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Infrastructure
{
    public class NotificationsContext : DbContext, IUnitOfWork
    {
        public NotificationsContext(DbContextOptions<NotificationsContext> options) : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NotificationEntityTypeConfiguration());

        }
    }
}
