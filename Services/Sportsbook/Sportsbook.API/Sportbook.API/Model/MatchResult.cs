using BettingApp.Services.Sportbook.API.Infrastructure.Exceptions;
using BettingApp.Services.Sportbook.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Model
{
    public class MatchResult : IEnumeration
    {
        public static MatchResult WinnerHomeClub = new MatchResult(1, nameof(WinnerHomeClub).ToLowerInvariant(), "1");
        public static MatchResult WinnerDraw = new MatchResult(2, nameof(WinnerDraw).ToLowerInvariant(), "Χ");
        public static MatchResult WinnerAwayClub = new MatchResult(3, nameof(WinnerAwayClub).ToLowerInvariant(), "2");
        public static MatchResult GoalsUnder = new MatchResult(4, nameof(GoalsUnder).ToLowerInvariant(), "Under");
        public static MatchResult GoalsOver = new MatchResult(5, nameof(GoalsOver).ToLowerInvariant(), "Over");
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string AliasName { get; set; }

        public MatchResult(int id, string name, string aliasName)
        {
            Id = id; 
            Name = name;
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
                throw new SportsbookDomainException($"Possible values for MatchResult: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static MatchResult From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new SportsbookDomainException($"Possible values for MatchResult: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
