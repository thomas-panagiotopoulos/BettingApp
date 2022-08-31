using BettingApp.Services.MatchSimulation.Domain.Exceptions;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate
{
    public class MatchResult : Enumeration
    {
        public static MatchResult WinnerHomeClub = new MatchResult(1,nameof(WinnerHomeClub).ToLowerInvariant(), 1,"winner");
        public static MatchResult WinnerDraw = new MatchResult(2, nameof(WinnerDraw).ToLowerInvariant(), 1, "winner");
        public static MatchResult WinnerAwayClub = new MatchResult(3, nameof(WinnerAwayClub).ToLowerInvariant(), 1, "winner");
        public static MatchResult GoalsUnder = new MatchResult(4, nameof(GoalsUnder).ToLowerInvariant(), 2,"goals");
        public static MatchResult GoalsOver = new MatchResult(5, nameof(GoalsOver).ToLowerInvariant(), 2, "goals");

        public int TypeId;
        public string TypeName;
        public MatchResult(int id, string name, int typeId, string typeName) : base(id, name)
        {
            TypeId = typeId;
            TypeName = typeName;
        }

        public static IEnumerable<MatchResult> List() =>
            new[] { WinnerHomeClub, WinnerDraw, WinnerAwayClub, GoalsUnder, GoalsOver };

        public static MatchResult FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new MatchSimulationDomainException($"Possible values for MatchResult: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static MatchResult From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new MatchSimulationDomainException($"Possible values for MatchResult: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
