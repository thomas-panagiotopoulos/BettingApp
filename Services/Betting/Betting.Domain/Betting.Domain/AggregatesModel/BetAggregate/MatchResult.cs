using BettingApp.Services.Betting.Domain.Seedwork;
using BettingApp.Services.Betting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate
{
    public class MatchResult : Enumeration
    {
        public static MatchResult WinnerHomeClub = new MatchResult(1, "WinnerHomeClub",
                                                                   1, "Winner",
                                                                   "1");
        public static MatchResult WinnerDraw = new MatchResult(2, "WinnerDraw",
                                                               1, "Winner",
                                                               "X");
        public static MatchResult WinnerAwayClub = new MatchResult(3, "WinnerAwayClub",
                                                                   1, "Winner",
                                                                   "2");
        public static MatchResult GoalsUnder = new MatchResult(4, "GoalsUnder",
                                                               2, "Goals",
                                                               "Under");
        public static MatchResult GoalsOver = new MatchResult(5, "GoalsOver",
                                                              2, "Goals",
                                                              "Over");

        public int TypeId;
        public string TypeName;
        public string AliasName;
        public MatchResult(int id, string name, int typeId, string typeName, string aliasName) : base(id, name)
        {
            TypeId = typeId;
            TypeName = typeName;
            AliasName = aliasName;
        }


        public static IEnumerable<MatchResult> List() =>
            new[] { WinnerHomeClub, WinnerDraw, WinnerAwayClub, GoalsUnder, GoalsOver };

        public static MatchResult FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new BettingDomainException($"Possible values for MatchResult: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static MatchResult From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new BettingDomainException($"Possible values for MatchResult: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
