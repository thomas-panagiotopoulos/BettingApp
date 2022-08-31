using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Infrastructure.Exceptions
{
    public class SportsbookDomainException : Exception
    {
        public SportsbookDomainException()
        { }

        public SportsbookDomainException(string message)
            : base(message)
        { }

        public SportsbookDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
