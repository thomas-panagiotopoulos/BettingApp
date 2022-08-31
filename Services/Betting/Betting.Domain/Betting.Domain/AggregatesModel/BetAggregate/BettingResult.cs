using BettingApp.Services.Betting.Domain.Seedwork;
using BettingApp.Services.Betting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate
{
    public class BettingResult : Enumeration
    {
        public static BettingResult Won = new BettingResult(1, "Won");
        public static BettingResult Lost = new BettingResult(2, "Lost");
        
        public BettingResult(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<BettingResult> List() =>
            new[] { Won, Lost };

        public static BettingResult FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new BettingDomainException($"Possible values for BettingResult: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static BettingResult From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new BettingDomainException($"Possible values for BettingResult: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
