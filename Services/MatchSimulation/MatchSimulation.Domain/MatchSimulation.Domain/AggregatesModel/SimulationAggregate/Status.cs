using BettingApp.Services.MatchSimulation.Domain.Exceptions;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate
{
    public class Status : Enumeration
    {
        public static Status Pending = new Status(1, nameof(Pending).ToLowerInvariant());
        public static Status Ongoing = new Status(2, nameof(Ongoing).ToLowerInvariant());
        public static Status Completed = new Status(3, nameof(Completed).ToLowerInvariant());
        public static Status Canceled = new Status(4, nameof(Canceled).ToLowerInvariant());
        
        
        public Status(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Status> List() =>
            new[] { Pending, Ongoing, Completed, Canceled };

        public static Status FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new MatchSimulationDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static Status From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new MatchSimulationDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}

