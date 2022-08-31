using BettingApp.Services.Betslips.API.Application.Commands;
using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Validations
{
    public class AddSelectionCommandValidator : AbstractValidator<AddSelectionCommand>
    {
        public AddSelectionCommandValidator()
        {
            RuleFor(command => command.GamblerId).NotEmpty();
            RuleFor(command => command.GamblerMatchResultId).NotEmpty()
                                                            .Must(BeValidMatchResult)
                                                            .WithMessage("Selection GamblerMatchResultId value is not valid.");
            RuleFor(command => command.Odd).NotEmpty()
                                           .GreaterThanOrEqualTo(1);
            RuleFor(command => command.RelatedMatchId).NotEmpty();
            RuleFor(command => command.HomeClubName).NotEmpty();
            RuleFor(command => command.AwayClubName).NotEmpty();
            RuleFor(command => command.KickoffDateTime).NotEmpty();
            RuleFor(command => command.CurrentMinute).NotEmpty()
                                                     .Must(BeValidMinute)
                                                     .WithMessage("Selection match's CurrentMinute value is not valid.");
            RuleFor(command => command.HomeClubScore).GreaterThanOrEqualTo(0);
            RuleFor(command => command.AwayClubScore).GreaterThanOrEqualTo(0);
            RuleFor(command => command.RequirementTypeId).NotEmpty()
                                                         .Must(BeValidRequirementType)
                                                         .WithMessage("Selection RequirementTypeId value is not valid.");
            RuleFor(command => command.RequiredValue).GreaterThanOrEqualTo(0);

        }

        private bool BeValidMatchResult(int matchResultId)
        {
            return MatchResult.List().Any(mr => mr.Id == matchResultId);
        }

        private bool BeValidMinute(string minute)
        {
            var regex = new Regex("^(((([4][5])|([9][0]))[+][1-9][0-9]*)|([1-8][0-9]?)|[0]|[9]|([9][0])|([H][T])|([F][T]))$");

            if (regex.IsMatch(minute)) return true;

            return false;
        }

        private bool BeValidRequirementType(int requirementTypeId)
        {
            return RequirementType.List().Any(rt => rt.Id == requirementTypeId);
        }
    }
}
