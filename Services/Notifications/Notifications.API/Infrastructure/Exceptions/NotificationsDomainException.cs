using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Infrastructure.Exceptions
{
    public class NotificationsDomainException : Exception
    {
        public NotificationsDomainException()
        { }

        public NotificationsDomainException(string message)
            : base(message)
        { }

        public NotificationsDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
