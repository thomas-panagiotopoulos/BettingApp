using System;

namespace BettingApp.Services.Betting.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class BettingDomainException : Exception
    {
        public BettingDomainException()
        { }

        public BettingDomainException(string message)
            : base(message)
        { }

        public BettingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
