using BettingApp.Services.Betting.Domain.Seedwork;
using BettingApp.Services.Betting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate
{
    public class Status : Enumeration
    {
        public static Status Pending = new Status(1, "Pending");
        public static Status Ongoing = new Status(2, "Ongoing");
        public static Status Completed = new Status(3, "Completed");
        public static Status Canceled = new Status(4, "Canceled");
        
        
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
                throw new BettingDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static Status From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new BettingDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}

