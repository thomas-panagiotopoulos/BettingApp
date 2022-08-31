using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Validations
{
    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        public CreateMatchCommandValidator()
        {
            RuleFor(command => command.MatchId).NotEmpty();
            RuleFor(command => command.SimulationId).NotEmpty();
            RuleFor(command => command.HomeClub).SetValidator(new ClubValidator());
            RuleFor(command => command.AwayClub).SetValidator(new ClubValidator());
            RuleFor(command => command.LeagueId).Must(BeValidLeagueId)
                                                .WithMessage("LeagueId value is not valid.");
            RuleFor(command => command.KickoffDateTime).Must(BeLaterDateTime)
                                                       .WithMessage("KickoffDateTime is not later than current DateTime.");
            RuleForEach(command => command.PossiblePicks).SetValidator(new PossiblePickValidator());
        }

        private bool BeValidLeagueId(int leagueId)
        {
            return League.List().Any(l => l.Id == leagueId);
        }

        private bool BeLaterDateTime(DateTime dateTime)
        {
            return DateTime.Compare(DateTime.UtcNow.AddHours(2), dateTime) < 0;
        }
    }

    public class PossiblePickValidator : AbstractValidator<PossiblePick>
    {
        public PossiblePickValidator()
        {
            RuleFor(possiblePick => possiblePick.Id).NotEmpty();
            RuleFor(possiblePick => possiblePick.MatchId).NotEmpty();
            RuleFor(possiblePick => possiblePick.MatchResultId).Must(BeValidMatchResult)
                                                                .WithMessage("PossiblePick's MatchResultId value is not valid.");
            RuleFor(possiblePick => possiblePick.Odd).GreaterThanOrEqualTo(1);
            RuleFor(possiblePick => possiblePick.RequirementTypeId).Must(BeValidRequirementType)
                                                                   .WithMessage("PossiblePick's RequirementTypeId value is not valid.");
            RuleFor(possiblePick => possiblePick.RequiredValue).GreaterThanOrEqualTo(0);
        }

        private bool BeValidMatchResult(int matchResultId)
        {
            return MatchResult.List().Any(mr => mr.Id == matchResultId);
        }

        private bool BeValidRequirementType(int requirementTypeId)
        {
            return RequirementType.List().Any(rt => rt.Id == requirementTypeId);
        }
    }
}
