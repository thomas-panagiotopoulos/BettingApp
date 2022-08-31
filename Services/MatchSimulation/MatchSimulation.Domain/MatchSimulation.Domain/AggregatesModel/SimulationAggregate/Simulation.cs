using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.Events;
using BettingApp.Services.MatchSimulation.Domain.Exceptions;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate
{
    public class Simulation : Entity, IAggregateRoot
    {
        // Id

        //public Match Match => _match;
        //private readonly Match _match;

        public string MatchId => _matchId;
        private string _matchId;

        public string CurrentMinute => _currentMinute;
        private string _currentMinute;

        public int CurrentMinuteInt => _currentMinuteInt;
        private int _currentMinuteInt;

        public int HomeClubScore => _homeClubScore;
        private int _homeClubScore;

        public int AwayClubScore => _awayClubScore;
        private int _awayClubScore;

        public int GoalsScored => _goalsScored;
        private int _goalsScored;

        public int RemainingExtraTimeMinutes => _remainingExtraTimeMinutes;
        private int _remainingExtraTimeMinutes;

        public int CurrentExtraTimeMinute => _currentExtraTimeMinute;
        private int _currentExtraTimeMinute;

        public int MinutesPassedInHalfTime => _minutesPassedInHalfTime;
        private int _minutesPassedInHalfTime;

        public Status Status { get; private set; }

        public int StatusId => _statusId;
        private int _statusId;

        public string StatusName => _statusName;
        private string _statusName;


        // constuctors

        protected Simulation()
        {
            Id = Guid.NewGuid().ToString();
            _currentMinuteInt = 0;
            _currentMinute = "0";
            _homeClubScore = 0;
            _awayClubScore = 0;
            _goalsScored = 0;
            _remainingExtraTimeMinutes = 0;
            _currentExtraTimeMinute = 0;
            _minutesPassedInHalfTime = 0;
            SetPendingStatus();
        }

        public Simulation(string matchId, Club homeClub, Club awayClub, int leagueId, DateTime kickoffDateTime) : this()
        {
            _matchId = matchId;

            // some domain level business validation
            if (homeClub.Id == awayClub.Id)
                throw new MatchSimulationDomainException("A Simulation cannot have the same Club for both HomeClub and AwayClub.");

            // First create the PossiblePicks list (internally calculates odds for each PossiblePick)
            var possiblePicks = CreatePossiblePicksForMatch(matchId, homeClub, awayClub);

            // Add a SimulationCreatedDomainEvent (containing required data to create a Match)
            AddDomainEvent(new SimulationCreatedDomainEvent(matchId, this.Id, homeClub, awayClub, leagueId,
                                                            kickoffDateTime, possiblePicks));
        }


        // class methods (ProgressByOneMinute, RandomizeScoresChange, CalculateNewOdds, etc)

        // method that creates a List of PossiblePicks (one for each MatchResult available) for a specific Match.
        // this method is meant to be used by the Simulation's constructor.
        private List<PossiblePick> CreatePossiblePicksForMatch(string matchId, Club homeClub, Club awayClub)
        {
            var possiblePicks = new List<PossiblePick>();
            int reqTypeId;
            decimal odd, reqValue;

            foreach (var matchResult in MatchResult.List())
            {
                odd = CalculateOddForPossiblePick(homeClub, awayClub, 0, 0, 90, matchResult.Id);
                reqTypeId = CreateRequirementType(odd);
                reqValue = CreateRequiredValue(reqTypeId);

                possiblePicks.Add(new PossiblePick(matchId, matchResult.Id, odd, reqTypeId, reqValue));
            }

            return possiblePicks;
        }

        // method that calculates a requirement type Id (for a PossiblePick) based on a given odd
        private int CreateRequirementType(decimal odd)
        {
            if (odd <= (decimal)1.30)
            {
                // if odd is too low, require minimum selection
                return RequirementType.MinimumSelections.Id;
            }
            else if (odd >= (decimal)10.0)
            {
                // if odd is too high, require maximum wagered amount
                return RequirementType.MaximumWageredAmount.Id;
            }
            else
            {
                // in any other case, reutn a random requirement
                var rnd = RandomDoubleInRange(0,1);
                if (rnd<=0.6)
                {
                    // 60% chance
                    return RequirementType.NoRequirement.Id;
                }
                else if (rnd <=0.70)
                {
                    // 10% chance
                    return RequirementType.MinimumSelections.Id;
                }
                else if (rnd <= 0.85)
                {
                    // 15% chance
                    return RequirementType.MinimumWageredAmount.Id;
                }
                else if (rnd <= 0.95)
                {
                    // 10% chance
                    return RequirementType.MaximumSelections.Id;
                }
                else if (rnd <= 1.00)
                {
                    // 5% chance
                    return RequirementType.MaximumWageredAmount.Id;
                }

                // this line should never be executed, but we add it for security reasons
                return RequirementType.NoRequirement.Id;
            }
        }

        // method that calculates a required value (for a PossiblePick) based on a given requirement type Id
        private decimal CreateRequiredValue(int requirementTypeId)
        {
            if (requirementTypeId == RequirementType.NoRequirement.Id)
            {
                return 0;
            }
            else if (requirementTypeId == RequirementType.MinimumSelections.Id)
            {
                return (RandomDoubleInRange(0, 1) >= 0.80) ? ((RandomDoubleInRange(0, 1) >= 0.60) ? 2 : 3) : 4;
            }
            else if (requirementTypeId == RequirementType.MinimumWageredAmount.Id)
            {
                return (RandomDoubleInRange(0, 1) >= 0.80) ? ((RandomDoubleInRange(0, 1) >= 0.60) ? 10 : 20) : 30;
            }
            else if (requirementTypeId == RequirementType.MaximumSelections.Id)
            {
                return (RandomDoubleInRange(0, 1) >= 0.80) ? ((RandomDoubleInRange(0, 1) >= 0.60) ? 4 : 3) : 2;
            }
            else if (requirementTypeId == RequirementType.MaximumWageredAmount.Id)
            {
                return (RandomDoubleInRange(0, 1) >= 0.80) ? ((RandomDoubleInRange(0, 1) >= 0.60) ? 50 : 40) : 20;
            }
            else
            {
                throw new MatchSimulationDomainException("Given RequirementType Id was not found.");
            }
        }

        // basic method to be used by 'ProgressMatchSimulationsJob'.
        // it advances the child Match's minute, calculates if any of the Clubs scored, calculates new PossiblePicks odds
        // and also disable the PossiblePicks with very high or very low odds, etc.
        public void ProgressByOneMinute(Club homeClub, Club awayClub)
        {
            // first check if Status is not Completed nor Canceled
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new MatchSimulationDomainException("Cannot progress a Simulation which is Completed or Canceled.");
            }

            // if Status == Pending then Link Home and Away clubs with Simulation
            if (_statusId == Status.Pending.Id)
            {
                // Add a SimulationStartedDomainEvent (Links Clubs with Simulation)
                AddDomainEvent(new SimulationStartedDomainEvent(this.Id));
            }
            // else, ensure that Simulation and given Clubs are related by checking their MatchId and ActiveMatchId
            else if (!_matchId.Equals(homeClub.ActiveMatchId) || !_matchId.Equals(awayClub.ActiveMatchId))
            {
                throw new MatchSimulationDomainException("Not all of Simulation, Match and Clubs are related with each other.");
            }


            // advance match's time by one minute
            // (internally RecalculateStatus and may add MatchNextMinuteCalculated domain event)
            AdvanceMatchByOneMinute();
            

            // check if status is Completed or Ongoing (proceed only if it is Ongoing)
            if (_statusId == Status.Completed.Id)
            {
                // Add a SimulationCompletedDomainEvent (Updates Club's Form and then Unlinks them from the Simulation)
                AddDomainEvent(new SimulationCompletedDomainEvent(this.Id, _matchId));
                return;
            }

            // only procceed with further tasks if match is NOT at Half Time
            if (!_currentMinute.Equals("HT"))
            {
                // calculates new club scores for related Match
                // (may internally add MatchNextScoresCalculatedDomainEvent)
                RandomizeMatchGoalScoring(homeClub, awayClub);

                // calculate new possible picks Odd values and Status (enabled/disabled)
                // (internally adds multiple MatchPossiblePickNewOddAndStatusCalculatedDomainEvent)
                CalculateMatchPossiblePicksNewOddsAndStatus(homeClub, awayClub);
            }
        }

        // method that cancels a Simulation along with its child Match
        public void Cancel()
        {
            // first check if Status is not Completed nor Canceled
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new MatchSimulationDomainException("Cannot cancel a Simulation which is Completed or Canceled.");
            }

            // Add a SimulationCanceledDomainEvent (Cancels related Match and Unlinks involving Clubs if status was Ongoing)
            AddDomainEvent(new SimulationCanceledDomainEvent(this.Id, _matchId, _statusId == Status.Ongoing.Id));
            
            // finally, set Status to Canceled
            SetCanceledStatus();   
        }

        // helper method that links match's Clubs with this Simulation
        // (after the Simulation has just started)
        // note: this method updates only the Clubs, not the Simulation
        public void LinkInvolvingClubsWithSimulation(Club homeClub, Club awayClub)
        {
            // first check if Status is Ongoing and CurrentMinute == 1. This means that Simulation has just
            // started, thus it is acceptable to link Clubs with it.
            if ( !(_statusId == Status.Ongoing.Id && _currentMinuteInt == 1))
            {
                throw new MatchSimulationDomainException("Cannot link Clubs with a Simulation that has not just started (Status = Ongoing and CurrentMinute = 1).");
            }

            // If everything is alright, proceed with linking
            homeClub.LinkWithSimulation(this.Id, _matchId);
            awayClub.LinkWithSimulation(this.Id, _matchId);
        }

        // helper method that unlinks match's Clubs from this Simulation
        // (after the Simulation is either Completer or Canceled)
        // note: this method updates only the Clubs, not the Simulation
        public void UnlinkInvolvingClubsFromSimulation(Club homeClub, Club awayClub)
        {
            // first chech if Status is Completed or Canceled
            // (if Simulation is at any other status, we should not unlink the clubs)
            if (_statusId != Status.Completed.Id && _statusId != Status.Canceled.Id)
            {
                throw new MatchSimulationDomainException("Clubs can be unlinked from the Simulation only if Simulation's Status is either Completed or Canceled.");
            }

            // if everything is alright, procceed with unlinking
            homeClub.UnlinkFromSimulation(this.Id);
            awayClub.UnlinkFromSimulation(this.Id);
        }

        // helper method that updates Clubs' Form stat accordingly after Match's completion
        // (after the Simulation is Completed)
        // note: this method updates only the Clubs, not the Simulation
        public void UpdateClubsFormAfterMatchCompletion(Club homeClub, Club awayClub)
        {
            // first check if Status is Completed (if Simulation is at any other status, we should not update the clubs)
            if (_statusId != Status.Completed.Id)
            {
                throw new MatchSimulationDomainException("Cannot update the Clubs' Form if related Simulation has not Completed status.");
            }

            // ensure that Simulation and given Clubs are related by checking their MatchId and ActiveMatchId
            if (!_matchId.Equals(homeClub.ActiveMatchId) || !_matchId.Equals(awayClub.ActiveMatchId))
            {
                throw new MatchSimulationDomainException("Not all of Simulation, Match and Clubs are related with each other.");
            }

            if (_homeClubScore > _awayClubScore)
            {
                // Home Club won
                homeClub.UpdateFormAfterWin(this.Id);
                awayClub.UpdateFormAfterLoss(this.Id);
            }
            else if (_homeClubScore < _awayClubScore)
            {
                // Away Club won
                homeClub.UpdateFormAfterLoss(this.Id);
                awayClub.UpdateFormAfterWin(this.Id);
            }
        }

        // method that advances the child Match's currentMinute by 1 (it takes into account the minutes the Match should
        // spend during the Half Time break, as well the Extra Time minutes).
        private void AdvanceMatchByOneMinute()
        {
            if (_currentMinute.Equals("FT")) 
                throw new MatchSimulationDomainException("Match is completed (CurrentMinute: \"FT\").");

            //string currentMinute = String.Empty;

            if (_currentMinute.Equals("HT"))
            {
                // if we are in Half time
                if (_minutesPassedInHalfTime < 15)
                {
                    // wait until 15 minutes pass in Half time
                    _minutesPassedInHalfTime++;
                    return;
                }
                else
                {
                    // after 15 minutes, begin second half (HT --> 46)
                    _currentMinuteInt++;
                    _currentMinute = _currentMinuteInt.ToString();
                }
            }
            else if (_currentMinuteInt == 45 || _currentMinuteInt == 90)
            {
                // handle extra time
                if (_remainingExtraTimeMinutes > 0)
                {
                    _currentExtraTimeMinute++;
                    _remainingExtraTimeMinutes--;
                    _currentMinute = _currentMinuteInt.ToString() + "+" + _currentExtraTimeMinute.ToString(); 
                }
                else
                {
                    _currentMinute = _currentMinuteInt == 45 ? "HT" : "FT";
                }
            }
            else
            {
                _currentMinuteInt++;
                _currentMinute = _currentMinuteInt.ToString();

                if (_currentMinuteInt == 45 || _currentMinuteInt == 90)
                {
                    // if we reach either 45th or 90th minute, calculate extraTime for next CurrentMinute update
                    CalculateExtraTime();
                }
            }

            // Add a MatchNewCurrentMinuteCalculatedDomainEvent domain event (containing currentMinute)
            AddDomainEvent(new MatchNewCurrentMinuteCalculatedDomainEvent(_matchId, _currentMinute));

            // finally recalculate Status
            CalculateStatus();
        }

        // helper method for AdvanceMatchByOneMinute metod
        private void CalculateExtraTime()
        {
            var baseExtraTime = (RandomIntInRange(1, 100) >= 40) ? 1 : 2;
            _remainingExtraTimeMinutes = Convert.ToInt32( Math.Ceiling(_goalsScored * 0.75 + baseExtraTime) );
            // reset goalsScored and currentExtraTimeMinute
            _goalsScored = 0;
            _currentExtraTimeMinute = 0;
        }


        // method that calculates if any of the clubs scored a goal, and Updates the Match's score accordingly
        private void RandomizeMatchGoalScoring(Club homeClub, Club awayClub)
        {
            // calculate scoring probabilities for each club
            var homeClubScoringProb = CalculateScoringProbability(homeClub.AttackStat, awayClub.DefenceStat,
                                                                  homeClub.FormStat, awayClub.FormStat, isHomeClub: true);
            var awayClubScoringProb = CalculateScoringProbability(awayClub.AttackStat, homeClub.DefenceStat,
                                                                  awayClub.FormStat, homeClub.FormStat, isHomeClub: false);
            
            // calculate scoring chances count for each club
            var homeClubChancesCount = Math.Truncate(homeClubScoringProb * 10000);
            var awayClubChancesCount = Math.Truncate(awayClubScoringProb * 10000);

            // create a scoringChancesResolver, which will contain both HomeClub and AwayClub scoring chances
            Dictionary<int, string> scoringChancesResolver = new Dictionary<int, string>();
            int randomChance;

            // populate scoringChancesResolver with HomeClub chances
            while (homeClubChancesCount > 0)
            {
                randomChance = RandomIntInRange(1, 10000);
                if (!scoringChancesResolver.ContainsKey(randomChance))
                {
                    scoringChancesResolver[randomChance] = homeClub.Id;
                    homeClubChancesCount--;
                }
            }

            // populate scoringChancesResolver with AwayClub chances
            while (awayClubChancesCount > 0)
            {
                randomChance = RandomIntInRange(1, 10000);
                if (!scoringChancesResolver.ContainsKey(randomChance))
                {
                    scoringChancesResolver[randomChance] = awayClub.Id;
                    awayClubChancesCount--;
                }
            }

            // finally, pick a random number (representing a random chance) and check if any Club has this number
            // in scoringChanceResolver to decide if any of the club scored a goal. If the number is not present
            // in the scoringChanceResolver, then none of the clubs scored.

            randomChance = RandomIntInRange(1,10000);

            if (scoringChancesResolver.ContainsKey(randomChance))
            {
                if (scoringChancesResolver[randomChance].Equals(homeClub.Id))
                {
                    // HomeClub scored
                    _homeClubScore++;
                    _goalsScored++;                    
                }
                else if (scoringChancesResolver[randomChance].Equals(awayClub.Id))
                {
                    // AwayClub scored
                    _awayClubScore++;
                    _goalsScored++;
                }
                // Add a MatchNewScoresCalculatedDomainEvent
                AddDomainEvent(new MatchNewScoresCalculatedDomainEvent(_matchId, _homeClubScore, _awayClubScore));
            }
            else
            {
                // if we get here, this means none of the clubs scored. Do nothing
            }

        }


        // method that Caculates a new Odd value for each PossiblePick and also Calculates the new Status
        // (enabled/disabled) for each PossiblePick based on it's new Odd value
        private void CalculateMatchPossiblePicksNewOddsAndStatus(Club homeClub, Club awayClub)
        {
            // pre-defined extreme value thresholds
            decimal lowerOddThreshold = 1.02m, upperOddThreshold = 100.00m; 

            foreach (var matchResult in MatchResult.List())
            {
                var newOdd = CalculateOddForPossiblePick(homeClub, awayClub,
                                                        _homeClubScore, _awayClubScore,
                                                        90 - _currentMinuteInt + _remainingExtraTimeMinutes, 
                                                        matchResult.Id);

                var isExtremeValue = newOdd < lowerOddThreshold || newOdd > upperOddThreshold;

                // Add a PossiblePickNewOddAndStatusCalculatedDomainEvent
                AddDomainEvent(new PossiblePickNewOddAndStatusCalculatedDomainEvent(_matchId, matchResult.Id, 
                                                                                         newOdd, isExtremeValue));
            }
        }

        // method that calculates the odd for a PossiblePick, based on stats of participating Clubs, current minute and
        // Club scores of the Match
        private decimal CalculateOddForPossiblePick(Club homeClub, Club awayClub, int homeClubScore, int awayClubScore,
                                                    int remainingMinutes, int matchResultId)
        {
            var homeClubScoringProb = CalculateScoringProbability(homeClub.AttackStat, awayClub.DefenceStat,
                                                                  homeClub.FormStat, awayClub.FormStat, isHomeClub:true);
            var awayClubScoringProb = CalculateScoringProbability(awayClub.AttackStat, homeClub.DefenceStat,
                                                                  awayClub.FormStat, homeClub.FormStat, isHomeClub:false);

            double matchResultProbability = 1, matchResultOdd;

            if (matchResultId == MatchResult.WinnerHomeClub.Id)
            {
                // calculate probability HomeClub to be ahead in score at the end of the match
                matchResultProbability = CalculateProbabilityClubAToBeAheadOfClubBInScoreAfterNMinutes(
                                                                                        homeClubScoringProb,
                                                                                        awayClubScoringProb,
                                                                                        remainingMinutes,
                                                                                        homeClubScore - awayClubScore);
            }
            else if (matchResultId == MatchResult.WinnerDraw.Id)
            {
                // calculate probablity HomeClub and AwayClub to have the same score at the end of the match
                matchResultProbability = CalculateProbabilityClubAAndClubBToHaveTheSameScoreAfterNMinutes(
                                                                                        homeClubScoringProb,
                                                                                        awayClubScoringProb,
                                                                                        remainingMinutes,
                                                                                        homeClubScore - awayClubScore);
                // alternate, 1 - probWinnerHomeClub - probWinnerAwayClub
            }
            else if (matchResultId == MatchResult.WinnerAwayClub.Id)
            {
                // calculate probability AwayClub to be ahead in score at the end of the match
                matchResultProbability = CalculateProbabilityClubAToBeAheadOfClubBInScoreAfterNMinutes(
                                                                                        awayClubScoringProb,
                                                                                        homeClubScoringProb,
                                                                                        remainingMinutes,
                                                                                        awayClubScore - homeClubScore);
            }
            else if (matchResultId == MatchResult.GoalsUnder.Id)
            {
                matchResultProbability = CalculateProbabilityClubAAndClubBToScoreAtMostXGoalsCombinedAfterNMinutes(
                                                                                        homeClubScoringProb,
                                                                                        awayClubScoringProb,
                                                                                        2, // under score threshold
                                                                                        remainingMinutes,
                                                                                        homeClubScore + awayClubScore);
            }
            else if (matchResultId == MatchResult.GoalsOver.Id)
            {
                matchResultProbability = CalculateProbabilityClubAAndClubBToScoreAtLeastXGoalsCombinedAfterNMinutes(
                                                                                        homeClubScoringProb,
                                                                                        awayClubScoringProb,
                                                                                        3, // over score threshold
                                                                                        remainingMinutes,
                                                                                        homeClubScore + awayClubScore);
                // alternate, 1 - probUnder
            }
            else
            {
                throw new MatchSimulationDomainException("Given MatchResult Id was not found.");
            }

            if(matchResultProbability == 1 || matchResultProbability == 0)
            {
                return 1m; // if matchResult is certain (1) or impossible (0) to happen, then return Odd = 1
                          // so this specific matchResult will be disabled.

            }

            // to ensure the house has profit, we give a reduced odd than the actual one.
            // the percentage used to reduce the odd, depends on the odd's size
            matchResultOdd = (1.0 / matchResultProbability);
            if (matchResultOdd <= 5)
            {
                // if odd is between 1-5, reduce percentage is 0.97-0.92
                matchResultOdd = GetReducedOdd(0.92, 0.97, 1, 5, matchResultOdd);
            }
            else if (matchResultOdd <= 10)
            {
                // if odd is between 5-10, reduce percentage is 0.92-0.87
                matchResultOdd = GetReducedOdd(0.87, 0.92, 5, 10, matchResultOdd);
            }
            else if (matchResultOdd <= 20)
            {
                // if odd is between 10-20, reduce percentage is 0.87-0.80
                matchResultOdd = GetReducedOdd(0.80, 0.87, 10, 20, matchResultOdd);
            }
            else if (matchResultOdd <= 40)
            {
                // if odd is between 20-40, reduce percentage is 0.80-0.72
                matchResultOdd = GetReducedOdd(0.72, 0.80, 20, 40, matchResultOdd);
            }
            else if (matchResultOdd <= 70)
            {
                // if odd is between 40-70, reduce percentage is 0.72-0.62
                matchResultOdd = GetReducedOdd(0.62, 0.72, 40, 70, matchResultOdd);
            }
            else if (matchResultOdd <= 120)
            {
                // if odd is between 70-120, reduce percentage is 0.62-0.55
                matchResultOdd = GetReducedOdd(0.55, 0.62, 70, 120, matchResultOdd);
            }
            else if (matchResultOdd <= 200)
            {
                // if odd is between 120-200, reduce percentage is 0.55-0.50
                matchResultOdd = GetReducedOdd(0.50, 0.55, 120, 200, matchResultOdd);
            }
            else
            {
                // if odd is above 200, reduce percentage is 0.50
                matchResultOdd = matchResultOdd * 0.50;
            }

            // before returning the Odd, we round it down to 2 decimal places and convert it to decimal
            // we also make sure we never return a value less than 1
            return (matchResultOdd >= 1) ? Convert.ToDecimal(Math.Round(matchResultOdd, 2)) : 1m ;
        }

        // helper method for CalculateOddForPossiblePick
        private double GetReducedOdd(double lowerReducePrct, double upperReducePrct, 
                                    double lowerOddLmt, double upperOddLmt, double odd)
        {
            if (odd < lowerOddLmt) return odd * upperReducePrct;
            if (odd > upperReducePrct) return odd * lowerReducePrct;

            var oddRangeFraction = (odd - lowerOddLmt) / (upperOddLmt - lowerOddLmt);
            var reducePrct = upperReducePrct - oddRangeFraction * (upperReducePrct - lowerReducePrct);

            return odd * reducePrct;
        }

        // method that calculates the odd for ClubA to be ahead of ClubB in score after N minutes.
        // parameters given are: scoring probabiblity per minute for ClubA (probA), scoring probability per minute
        // for ClubB (probB), remaining play time in minutes (N) and current score difference (scoreDifference).
        // If scoreDifference is >0, it means ClubA is currently ahead in score, if scoreDiffernece is <0
        // then ClubB is ahead in score, and if it's equal to 0 then both clubs have the same score.
        // It is considered that only one club can score in every minute.
        private double CalculateProbabilityClubAToBeAheadOfClubBInScoreAfterNMinutes(double probA, double probB, 
                                                                                     int N, int scoreDifference)
        {
            double probNone = 1 - probA - probB;
            double finalProb = 0, currentProb, arrangementsCount;
            int goalsA, goalsB, goalsNone;

            for (goalsA = N; goalsA >= 0; goalsA--)
            {
                goalsB = 0;
                while (goalsB <= N - goalsA)
                {
                    if (goalsA > goalsB - scoreDifference)
                    {
                        goalsNone = N - goalsA - goalsB;

                        currentProb = Math.Pow(probA, goalsA) * Math.Pow(probB, goalsB) * Math.Pow(probNone, goalsNone);
                        arrangementsCount = CalculateArrangementsCount(goalsA, goalsB, goalsNone);
                        finalProb += arrangementsCount * currentProb;
                    }

                    goalsB++;
                }
            }

            return finalProb;
        }

        // method that calculates the odd for ClubA and ClubB to have the same score after N minutes.
        // parameters given are: scoring probabiblity per minute for ClubA (probA), scoring probability per minute
        // for ClubB (probB), remaining play time in minutes (N) and current score difference (scoreDifference).
        // If scoreDifference is >0, it means ClubA is currently ahead in score, if scoreDiffernece is <0
        // then ClubB is ahead in score, and if it's equal to 0 then both clubs have the same score.
        // It is considered that only one club can score in every minute.
        private double CalculateProbabilityClubAAndClubBToHaveTheSameScoreAfterNMinutes(double probA, double probB, int N, int scoreDifference)
        {
            double probNone = 1 - probA - probB;
            double finalProb = 0, currentProb, arrangementsCount;
            int goalsA, goalsB, goalsNone;

            for (goalsA = 0; goalsA <= N; goalsA++)
            {
                goalsB = goalsA + scoreDifference;

                if (goalsB < 0 || goalsB > N || goalsB > N - goalsA) continue;

                goalsNone = N - goalsA - goalsB;

                currentProb = Math.Pow(probA, goalsA) * Math.Pow(probB, goalsB) * Math.Pow(probNone, goalsNone);
                arrangementsCount = CalculateArrangementsCount(goalsA, goalsB, goalsNone);
                finalProb += arrangementsCount * currentProb;
            }

            return finalProb;
        }

        // method that calculates the odd for ClubA and Club to score combined at most X goals after N minutes
        // parameters given are: scoring probabiblity per minute for ClubA (probA), scoring probability per minute
        // for ClubB (probB), maximum amount of goals to be scored by both clubs combined (X), remaining play time
        // in minutes (N) and current goals scored in match by both clubs combined (currentCombinedScore).
        private double CalculateProbabilityClubAAndClubBToScoreAtMostXGoalsCombinedAfterNMinutes(
                                                                                            double probA, double probB, 
                                                                                            int X, int N, 
                                                                                            int currentCombinedScore)
        {
            double probNone = 1 - probA - probB, probCombined = probA + probB;
            double finalProb = 0, currentProb, arrangementsCount;
            int goalsCombined, goalsNone;

            if (currentCombinedScore > X) return 0;

            X -= currentCombinedScore;
            goalsCombined = 0;

            while(goalsCombined <= X && goalsCombined <= N)
            {
                goalsNone = N - goalsCombined;
                currentProb = Math.Pow(probCombined, goalsCombined) * Math.Pow(probNone, goalsNone);
                arrangementsCount = CalculateArrangementsCount(goalsCombined, 0, goalsNone);
                finalProb += arrangementsCount * currentProb;

                goalsCombined++;
            }

            return finalProb;
        }

        // method that calculates the odd for ClubA and Club to score combined at least X goals after N minutes
        // parameters given are: scoring probabiblity per minute for ClubA (probA), scoring probability per minute
        // for ClubB (probB), minimum amount of goals to be scored by both clubs combined (X), remaining play time
        // in minutes (N) and current goals scored in match by both clubs combined (currentCombinedScore).
        private double CalculateProbabilityClubAAndClubBToScoreAtLeastXGoalsCombinedAfterNMinutes(
                                                                                            double probA, double probB,
                                                                                            int X, int N, 
                                                                                            int currentCombinedScore)
        {
            double probNone = 1 - probA - probB, probCombined = probA + probB;
            double finalProb = 0, currentProb, arrangementsCount;
            int goalsCombined, goalsNone;

            if (currentCombinedScore >= X) return 1;

            X -= currentCombinedScore;
            goalsCombined = X;

            while (goalsCombined <= N)
            {
                goalsNone = N - goalsCombined;
                currentProb = Math.Pow(probCombined, goalsCombined) * Math.Pow(probNone, goalsNone);
                arrangementsCount = CalculateArrangementsCount(goalsCombined, 0, goalsNone);
                finalProb += arrangementsCount * currentProb;

                goalsCombined++;
            }

            return finalProb;
        }

        // helper method for calculate probability for a MatchResult to happen
        private double CalculateArrangementsCount(int goalsA, int goalsB, int goalsNone)
        {
            // we use double for supporting bigger number ranges than int
            double goalsADouble = Convert.ToDouble(goalsA), goalsBDouble = Convert.ToDouble(goalsB), goalsNoneDouble = Convert.ToDouble(goalsNone);
            double totalDouble = goalsADouble + goalsBDouble + goalsNoneDouble;
            double arrangemenets = Factorial(totalDouble) / ( Factorial(goalsADouble) * Factorial(goalsBDouble) * Factorial(goalsNoneDouble) );

            return arrangemenets;
        }
        
        // helper method that calculates the Factorial of a number
        private double Factorial(double a)
        {
            // we use double for supporting bigger number ranges than int
            if (a <= 1) return 1;

            double f = 1;
            for(var i = a; i>0; i--)
            {
                f *= i;
            }

            return f;
        }

        // method that calculates the probability for a Club to score against another Club. It uses the two involving
        // clubs Stats.
        private double CalculateScoringProbability(int attackStat, int defenceStat, 
                                                    int attackingForm, int defendingForm, bool isHomeClub)
        {
            //var resultProbabilityResolver = new Dictionary<double, double>()
            //{
            //    {18, 1/8.0}, {17, 1/9.7}, {16, 1/10.8}, {15, 1/13.6}, {14, 1/18.4}, {13, 1/24.2},
            //    {12, 1/30.004}, {11, 1/35.087}, {10, 1/40.17}, {9, 1/45.253}, {8, 1/50.336}, {7, 1/55.419},
            //    {6, 1/60.502}, {5, 1/65.58}, {4, 1/70.668}, {3, 1/75.751}, {2, 1/80.834}, {1, 1/85.917},
            //    {0, 1/91.0}, {-1, 1/100.08}, {-2, 1/109.16}, {-3, 1/118.24}, {-4, 1/127.32}, {-5, 1/137.4},
            //    {-6, 1/147.48}, {-7, 1/157.56}, {-8, 1/167.64}, {-9, 1/178.72}, {-10, 1/189.8}, {-11, 1/200.88},
            //    {-12, 1/211.96}, {-13, 1/238.96}, {-14, 1/275.96}, {-15, 1/312.96}, {-16, 1/360.0}, {-17, 1/540.0},
            //    {-18, 1/720.0}
            //};

            var resultProbabilityResolver = new Dictionary<double, double>()
            {
                {-38, 1/720.0}, {-37, 1/585.00}, {-36, 1/540.00}, {-35, 1/456.00}, {-34, 1/372.00}, {-33, 1/352.32},
                {-32, 1/332.64}, {-31, 1/312.96}, {-30, 1/294.46}, {-29, 1/275.960}, {-28, 1/257.580}, {-27, 1/239.200},
                {-26, 1/1/226.000}, {-25, 1/212.800}, {-24, 1/207.160}, {-23, 1/201.520}, {-22, 1/195.880}, {-21, 1/190.240},
                {-20, 1/184.600}, {-19, 1/178.960}, {-18, 1/173.320}, {-17, 1/167.680}, {-16, 1/162.040}, {-15, 1/157.200},
                {-14, 1/152.360}, {-13, 1/147.520}, {-12, 1/142.680}, {-11, 1/137.840}, {-10, 1/133.000}, {-9, 1/128.160},
                {-8, 1/1/123.320}, {-7, 1/119.280}, {-6, 1/115.240}, {-5, 1/111.200}, {-4, 1/107.160}, {-3, 1/103.120},
                {-2, 1/99.080}, {-1, 1/95.040},
                {0, 1/91.000},
                {1, 1/88.566}, {2, 1/86.132}, {3, 1/83.698}, {4, 1/81.264}, {5, 1/78.830}, {6, 1/76.396},
                {7, 1/73.962}, {8, 1/71.528}, {9, 1/69.094}, {10, 1/66.660}, {11, 1/64.226}, {12, 1/61.792},
                {13, 1/59.358}, {14, 1/56.924}, {15, 1/54.490}, {16, 1/52.056}, {17, 1/49.622}, {18, 1/47.188},
                {19, 1/44.754}, {20, 1/42.320}, {21, 1/39.886}, {22, 1/37.452}, {23, 1/35.018}, {24, 1/32.418},
                {25, 1/29.818}, {26, 1/26.918}, {27, 1/24.018}, {28, 1/21.918}, {29, 1/19.818}, {30, 1/17.718},
                {31, 1/15.508}, {32, 1/13.298}, {33, 1/11.899}, {34, 1/10.500}, {35, 1/10.050}, {36, 1/9.600},
                {37, 1/8.800}, {38, 1/8.000}
            };

            // this value will be in range [-38,38]
            var scoringAbility = CaclculateScoringAbility(attackStat, defenceStat, attackingForm, defendingForm); 

            // homeClub gets a scoringAbility boost up to +1
            double homeBonusPercentage = isHomeClub ? RandomDoubleInRange(0,1): 0; 


            if (resultProbabilityResolver.ContainsKey(scoringAbility))
            {
                // avoid getting out of Dictionary key values if scoringAbility has max value (38)
                if (scoringAbility == 38) return resultProbabilityResolver[scoringAbility];

                // calculate boosted scoringProbability for homeClubs
                double nextProbDif = resultProbabilityResolver[scoringAbility + 1] - resultProbabilityResolver[scoringAbility];
                double finalProb = resultProbabilityResolver[scoringAbility] + homeBonusPercentage * nextProbDif;

                return finalProb;
                
            }
            else
            {
                return 0; // this should never happen, don't change the way 'result' is calculated 
                            // or resultProbabilityResolver's content
            }

        }

        // helper method for CalculateScoringProbability method
        public double CaclculateScoringAbility(int atk, int def, int atkForm, int defForm)
        {
            return atk - def + atkForm - defForm;
        }

        // helper method that produces a random double number in the given range
        private double RandomDoubleInRange(double minValue, double maxValue)
        {
            Random random = new Random();
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        // helper method that produces a random int number in the given range
        private int RandomIntInRange(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1);
        }

        // method that caclulates the Status of the Simulation, based on the related Match's currentMinute
        // and current Simulation's Status
        private void CalculateStatus()
        {
            // if Status is Canceled, don't update it
            if (_statusId == Status.Canceled.Id) return;

            if (_currentMinute.Equals("0"))
            {
                SetPendingStatus();
            }
            else if (_currentMinute.Equals("FT"))
            {
                SetCompletedStatus();
            }
            else
            {
                SetOngoingStatus();
            }
        }


        private void SetPendingStatus()
        {
            _statusId = Status.Pending.Id;
            _statusName = Status.Pending.Name;
        }

        private void SetOngoingStatus()
        {
            _statusId = Status.Ongoing.Id;
            _statusName = Status.Ongoing.Name;
        }

        private void SetCompletedStatus()
        {
            _statusId = Status.Completed.Id;
            _statusName = Status.Completed.Name;
        }

        private void SetCanceledStatus()
        {
            _statusId = Status.Canceled.Id;
            _statusName = Status.Canceled.Name;
        }
    }
}
