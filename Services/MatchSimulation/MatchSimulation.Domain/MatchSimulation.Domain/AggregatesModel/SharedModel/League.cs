using BettingApp.Services.MatchSimulation.Domain.Exceptions;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel
{
    public class League : Enumeration
    {
        public static League GreekSuperLeague = new League(1, nameof(GreekSuperLeague), 1, "domestic",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 17, 15, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 19, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 15, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 17, 15, 2),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 19, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Monday, 19, 30, 1),
                                                            });
        public static League EnglishPremierLeague = new League(2, nameof(EnglishPremierLeague), 1, "domestic",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Friday, 22, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 14, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 17, 00, 3),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 19, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 16, 00, 3),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 18, 30, 1),
                                                            });
        public static League SpanishLaLiga = new League(3, nameof(SpanishLaLiga), 1, "domestic",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Friday, 22, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 15, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 17, 15, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 19, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 22, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 15, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 17, 15, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 19, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 22, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Monday, 22, 00, 1),
                                                            });
        public static League ItalianSerieA = new League(4, nameof(ItalianSerieA), 1, "domestic",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 16, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 19, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 21, 45, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 22, 00, 1),                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 15, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 13, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 16, 00, 3),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 19, 00, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 21, 45, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Monday, 21, 45, 1),
                                                            });
        public static League GermanBundesliga = new League(5, nameof(GermanBundesliga), 1, "domestic",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Friday, 21, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 16, 30, 4),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Saturday, 19, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 16, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 18, 30, 1),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 20, 30, 1),
                                                            });
        public static League NoDomesticLeague = new League(6, nameof(NoDomesticLeague), 1, "domestic",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 00, 00, 0)
                                                            });

        public static League ChampionsLeague = new League(7, nameof(ChampionsLeague), 2, "continental",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Tuesday, 19, 45, 2),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Tuesday, 22, 00, 6),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Wednesday, 19, 45, 2),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Wednesday, 22, 00, 6),
                                                            });
        public static League EuropaLeague = new League(8, nameof(EuropaLeague), 2, "continental",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Thursday, 19, 45, 8),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Thursday, 22, 00, 8),
                                                            });
        public static League EuropaConferenceLeague = new League(9, nameof(EuropaConferenceLeague), 2, "continental",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Thursday, 19, 45, 8),
                                                                new MatchKickoffDayAndTime(DayOfWeek.Thursday, 22, 00, 8),
                                                            });
        public static League NoContinentalLeague = new League(10, nameof(NoContinentalLeague), 2, "continental",
                                                            new List<MatchKickoffDayAndTime>()
                                                            {
                                                                new MatchKickoffDayAndTime(DayOfWeek.Sunday, 00, 00, 0),
                                                            });

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public List<MatchKickoffDayAndTime> MatchKickoffDaysAndTimes { get; set; }

        public League(int id, string name, int typeId, string typeName) : base(id, name)
        {
            TypeId = typeId;
            TypeName = typeName;
        }

        public League(int id, string name, int typeId, string typeName, List<MatchKickoffDayAndTime> matchKickoffDaysAndTimes) 
            : this(id, name, typeId, typeName)
        {
            MatchKickoffDaysAndTimes = matchKickoffDaysAndTimes;
        }

        public static IEnumerable<League> List() =>
            new[] { GreekSuperLeague, EnglishPremierLeague, SpanishLaLiga, ItalianSerieA, GermanBundesliga, NoDomesticLeague,
                    ChampionsLeague, EuropaLeague, EuropaConferenceLeague, NoContinentalLeague};

        public static League FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new MatchSimulationDomainException($"Possible values for League: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static League From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new MatchSimulationDomainException($"Possible values for League: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }

    public class MatchKickoffDayAndTime
    {
        public DayOfWeek Day { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int MaxMatchesCount { get; set; }
        public MatchKickoffDayAndTime(DayOfWeek day, int hours, int minutes, int maxMatchesCount)
        {
            Day = day;
            Hours = hours;
            Minutes = minutes;
            MaxMatchesCount = maxMatchesCount;
        }
    }
}
