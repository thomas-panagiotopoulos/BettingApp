using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Exceptions
{
    public class MatchSimulationDomainException : Exception
    {
        public MatchSimulationDomainException()
        { }

        public MatchSimulationDomainException(string message)
            : base(message)
        { }

        public MatchSimulationDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
