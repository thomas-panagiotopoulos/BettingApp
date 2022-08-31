using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.BackgroundTasks.Jobs
{
    public class CreateNextSimulationsJob : IJob
    {
        private readonly ILogger<CreateNextSimulationsJob> _logger;
        private readonly IMediator _mediator;
        private readonly IClubRepository _clubRepository;
        private readonly IMatchRepository _matchRepository;

        public CreateNextSimulationsJob(ILogger<CreateNextSimulationsJob> logger,
                                        IMediator mediator,
                                        IClubRepository clubRepository,
                                        IMatchRepository matchRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _clubRepository = clubRepository;
            _matchRepository = matchRepository;
        }

        // This Job is scheduled to be executed once a day every, every day of the week at 00:01
        // On TUE,WED and THU, it attempts tp create Simulations for Domestic leagues (taking place on FRI,SAT,SUN and MON).
        // On FRI,SAT,SUN and MON, it attemps to create Simulations for Continental leagues (taking place on TUE,WED and THU).
        // Before creating any Simulations for the corresponding days, it checks if there are any Matches scheduled
        // on these days. If any Matches found, the execution of the Job is skipped in order to avoid creating
        // separate Simulations involving the same Club(s) in the same day.
        // It uses Kickoff Days and Times for each Simulation based on the info stored on Leagues.
        // Club pairs for each Simulation are randomly picked.

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation(".:: Starting execution of CreateNextSimulationsJob ::.");

            // first find what day of the week is currently
            var GmtPlus2Now = DateTime.UtcNow.AddHours(2);
            var dayOfWeek = GmtPlus2Now.DayOfWeek;

            // initialize a List to store Leagues we want to create Simulations for
            var desiredLeagues = new List<League>();

            // based on the day the Job is executed, we filter Leagues we want to create Simulations for.
            // on TUE,WED,THU we want to create Simulations for Domestic leagues for the upcoming FRI,SAT,SUN,MON.
            // on FRI,SAT,SUN,MON we want to create Simulations for Continental leagues for the upcoming TUE,WED,THU.
            // before filtering the desired Leagues though, we also check if the upcoming days contain any Simulations,
            // and only proceed if they contain zero Simulations.
            if (dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Wednesday || dayOfWeek == DayOfWeek.Thursday)
            {
                // check if there any scheduled Matches in the upcoming FRI,SAT,SUN or MON
                var anyMatchesOnFriday = await _matchRepository.ExistsWithKickoffDate(GmtPlus2Now.AddDays(DaysUntilNext(DayOfWeek.Friday)));
                var anyMatchesOnSaturday = await _matchRepository.ExistsWithKickoffDate(GmtPlus2Now.AddDays(DaysUntilNext(DayOfWeek.Saturday)));
                var anyMatchesOnSunday = await _matchRepository.ExistsWithKickoffDate(GmtPlus2Now.AddDays(DaysUntilNext(DayOfWeek.Sunday)));
                var anyMatchesOnMonday = await _matchRepository.ExistsWithKickoffDate(GmtPlus2Now.AddDays(DaysUntilNext(DayOfWeek.Monday)));

                // if any Matches are found, skip execution of the Job
                if (anyMatchesOnFriday || anyMatchesOnSaturday || anyMatchesOnSunday || anyMatchesOnMonday)
                {
                    _logger.LogInformation("Skipping execution of CreateNextSimulationsJob as there were found " +
                                            "scheduled Matches in the upcoming FRI to MON days.");
                    return;
                }

                // else procceed by filtering only the desired Leagues (Domestic)
                League.List().ToList()
                             .Where(l => l.TypeId == League.GreekSuperLeague.TypeId && l.Id != League.NoDomesticLeague.Id)
                             .ToList()
                             .ForEach(l => desiredLeagues.Add(l));
            }
            else if (dayOfWeek == DayOfWeek.Friday || dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Monday)
            {
                // check if there any scheduled Matches in the upcoming TUE, WED, or THU
                var anyMatchesOnTuesday = await _matchRepository.ExistsWithKickoffDate(GmtPlus2Now.AddDays(DaysUntilNext(DayOfWeek.Tuesday)));
                var anyMatchesOnWednesday = await _matchRepository.ExistsWithKickoffDate(GmtPlus2Now.AddDays(DaysUntilNext(DayOfWeek.Wednesday)));
                var anyMatchesOnThursday = await _matchRepository.ExistsWithKickoffDate(GmtPlus2Now.AddDays(DaysUntilNext(DayOfWeek.Thursday)));

                // if any Matches are found, skip execution of the Job
                if (anyMatchesOnTuesday || anyMatchesOnWednesday || anyMatchesOnThursday)
                {
                    _logger.LogInformation("SKipping execution of CreateNextSimulationsJob as there were found " +
                                            "scheduled Matches in the upcoming TUE to THU days.");
                    return;
                }

                // else procceed by filtering only the desired Leagues (Continental)
                League.List().ToList()
                             .Where(l => l.TypeId == League.ChampionsLeague.TypeId && l.Id != League.NoContinentalLeague.Id)
                             .ToList()
                             .ForEach(l => desiredLeagues.Add(l));
            }

            // declare the list that will hold the Clubs loaded for each League
            List<Club> clubs;

            // then run though all the Leagues to create Simulations
            foreach(var league in desiredLeagues)
            {

                // check the type of the League and load the appropriate Clubs from the repository
                if (league.TypeId == League.GreekSuperLeague.TypeId)
                {
                    clubs = await _clubRepository.GetClubsByDomesticLeagueId(league.Id);
                }
                else if (league.TypeId == League.ChampionsLeague.TypeId)
                {
                    clubs = await _clubRepository.GetClubsByContinentalLeagueId(league.Id);
                }
                else
                {
                    // if League type is not found, continue to next League (this should not happen)
                    continue;
                }

                // if no Clubs are found for this League or found Clubs are less than 2, continue to next League
                if (clubs == null || clubs.Count <2)
                    continue;

                // create a List of random ClubsPairs, based on the loaded Clubs, to create Simulations
                var clubPairs = CreateClubPairs(clubs);

                // schedule each ClubPair of the list by assigning them a KickoffDateTime from
                // the available KickoffDaysAndTimes provided by League
                var scheduledClubPairs = ScheduleClubPairs(clubPairs, league);

                // finally, we send a CreateSimulationCommand for each of the scheduledClubPairs
                foreach (var pair in scheduledClubPairs)
                {
                    var matchId = await CreateUniqueReadableId(length:6);
                    var homeClub = clubs.FirstOrDefault(c => c.Id.Equals(pair.HomeClubId));
                    var awayClub = clubs.FirstOrDefault(c => c.Id.Equals(pair.AwayClubId));
                    try
                    {
                        await _mediator.Send(new CreateSimulationCommand(matchId, homeClub, awayClub, 
                                                                        league.Id, pair.KickoffDateTime));
                    }
                    catch(Exception e)
                    {
                        // if an exception is thrown, log it and continue with next simulation
                        _logger.LogInformation($"ERROR while sending CreateSimulationCommand " +
                                                $"for Match with Id {matchId}: \"{e.Message}\"");
                    }
                }                
            }

            _logger.LogInformation(".:: Ending execution of CreateNextSimulationsJob ::.");
        }

        // helper method that creates a List of Club pairs based on given Clubs,
        // to be used in Simulations creation
        // note: if given Clubs count is not even, then the last Club will not be included in any pair
        private List<ClubPair> CreateClubPairs(List<Club> clubs)
        {
            // if given Clubs is null or less than 2, then return null (unable to create any pair)
            if (clubs == null || clubs.Count < 2)
                return null;

            // otherwise, first shuffle the Clubs list
            Random rng = new Random();
            List<Club> shuffledClubs = clubs.OrderBy(c => rng.Next()).ToList();

            // then create a List of ClubPairs by selecting the shuffled Clubs 'by two'
            // note: if given Clubs count is not even, we exclude the last Club to make the count even
            int clubsToBeUsed = (shuffledClubs.Count % 2 == 0) ? shuffledClubs.Count : shuffledClubs.Count - 1;
            List<ClubPair> clubPairs = new List<ClubPair>();
            for (var i = 0; i < clubsToBeUsed; i += 2)
            {
                clubPairs.Add(new ClubPair(shuffledClubs[i].Id, shuffledClubs[i + 1].Id));
            }

            // the list of ClubPairs is ready, we return it
            return clubPairs;
        }

        // helper method that schedules each of the given Club pairs with a KickoffDateTime,
        // based on the provided MatchKickoffDaysAndTimes of the League
        // note: if available MatchKickoffDaysAndTimes of League and given ClubPairs counts are not equal,
        // then we schedule as many Club pairs as the minimum of the two counts.
        private List<ClubPair> ScheduleClubPairs(List<ClubPair> clubPairs, League league)
        {
            // declare and initialize the List of scheduled ClubPairs to be returned
            List<ClubPair> scheduledClubPairs = new List<ClubPair>();
            //declare and initialize the index for clubPairs
            int cpIndex = 0;
            // create a DateTime representing today's Date that to be used anywhere inside the method
            var todayDate = DateTime.UtcNow.AddHours(2).Date;

            foreach (var matchKickoffDayAndTime in league.MatchKickoffDaysAndTimes)
            {
                for(var i = 0; i<matchKickoffDayAndTime.MaxMatchesCount; i++)
                {
                    // if all ClubPairs are scheduled, stop scheduling
                    if (!clubPairs.Any(cp => !cp.IsScheduled))
                        break;

                    // otherwise, assign a KickoffDateTime to the ClubPair
                    clubPairs[cpIndex].KickoffDateTime = todayDate.AddDays(DaysUntilNext(matchKickoffDayAndTime.Day))
                                                                  .AddHours(matchKickoffDayAndTime.Hours)
                                                                  .AddMinutes(matchKickoffDayAndTime.Minutes);
                    // mark it as scheduled
                    clubPairs[cpIndex].IsScheduled = true;
                    // add it to the Scheduled ClubPairs list
                    scheduledClubPairs.Add(clubPairs[cpIndex]);
                    // increase the ClubPairs index to point to the next available ClubPair
                    cpIndex++;
                }
            }

            return scheduledClubPairs;
        }

        // helper method that calculates how many days ahead from Today is the specified DayOfWeek
        private int DaysUntilNext(DayOfWeek day)
        {
            var todayDate = DateTime.UtcNow.AddHours(2).Date;

            return ( (int)day - (int)todayDate.DayOfWeek > 0 )
                        ? (int)day - (int)todayDate.DayOfWeek
                        : (int)day - (int)todayDate.DayOfWeek + 7;
        }

        // helper method that creates a Human-readable Id of given lenght, guaranting its uniquness
        // by querying the Repository before returning a value.
        // If a unique Id is not found after 100 tries, then it returns null
        private async Task<string> CreateUniqueReadableId(int length)
        {
            // initialize the characters to be used for Id, a Random number generator and a StringBuilder
            char[] _base62chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            Random _random = new Random();
            var sb = new StringBuilder(length);

            // create a random Id (use only base36 chars)
            for (int i = 0; i < length; i++)
                sb.Append(_base62chars[_random.Next(36)]);

            // if Id already exists in DB, then create a new one and check again,
            // until a unique one is created or max tries (100) are reached
            var tries = 0;
            while (await _matchRepository.ExistsWithIdAsync(sb.ToString()) && tries< 100)
            {
                sb.Clear();
                tries++;
                for (int i = 0; i < length; i++)
                    sb.Append(_base62chars[_random.Next(36)]);
            }

            // if total tries is at max value (100) , this means no unique Id was created, so we return null
            if (tries >= 100) return null;

            // if we got here, the created Id is unique and we can return it safely
            return sb.ToString();
        }
    }

    public class ClubPair
    {
        public string HomeClubId { get; set; }
        public string AwayClubId { get; set; }
        public DateTime KickoffDateTime { get; set; }
        public bool IsScheduled { get; set; }
        public ClubPair(string homeClubId, string awayClubId)
        {
            HomeClubId = homeClubId;
            AwayClubId = awayClubId;
            IsScheduled = false;
        }
    }
}
