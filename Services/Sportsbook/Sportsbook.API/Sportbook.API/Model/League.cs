using BettingApp.Services.Sportbook.API.Infrastructure.Exceptions;
using BettingApp.Services.Sportbook.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Model
{
    public class League : IEnumeration
    {
        public static League GreekSuperLeague = new League(1, nameof(GreekSuperLeague));
        public static League EnglishPremierLeague = new League(2, nameof(EnglishPremierLeague));
        public static League SpanishLaLiga = new League(3, nameof(SpanishLaLiga));
        public static League ItalianSerieA = new League(4, nameof(ItalianSerieA));
        public static League GermanBundesliga = new League(5, nameof(GermanBundesliga));
        public static League NoDomesticLeague = new League(6, nameof(NoDomesticLeague));

        public static League ChampionsLeague = new League(7, nameof(ChampionsLeague));
        public static League EuropaLeague = new League(8, nameof(EuropaLeague));
        public static League EuropaConferenceLeague = new League(9, nameof(EuropaConferenceLeague));
        public static League NoContinentalLeague = new League(10, nameof(NoContinentalLeague));


        public int Id { get; set; }
        public string Name { get; set; }

        public League(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<League> List() =>
            new[] { GreekSuperLeague, EnglishPremierLeague, SpanishLaLiga, ItalianSerieA, GermanBundesliga, NoDomesticLeague,
                    ChampionsLeague, EuropaLeague, EuropaConferenceLeague, NoContinentalLeague };

        public static League FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new SportsbookDomainException($"Possible values for League: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static League From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new SportsbookDomainException($"Possible values for League: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
