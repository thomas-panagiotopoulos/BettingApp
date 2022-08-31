using BettingApp.Services.Notifications.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Model
{
    public class Notification : IEntity
    {
        public string Id { get; private set; }

        public string GamblerId { get; private set; }

        public DateTime DateTimeCreated { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public bool IsRead { get; private set; }

        protected Notification()
        {
            Id = Guid.NewGuid().ToString();
            DateTimeCreated = DateTime.UtcNow.AddHours(2);
            IsRead = false;
        }

        public Notification(string gamblerId, string title, string description) : this()
        {
            GamblerId = gamblerId;
            Title = title;
            Description = description;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
