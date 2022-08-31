using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Validations
{
    public class UpdatePossiblePickCommandValidator : AbstractValidator<UpdatePossiblePickCommand>
    {
        public UpdatePossiblePickCommandValidator()
        {
            RuleFor(command => command.MatchId).NotEmpty();
            RuleFor(command => command.MatchResultId).Must(BeValidMatchResult)
                                                     .WithMessage("MatchResuldId value is not valid.");
            RuleFor(command => command.NewOdd).GreaterThanOrEqualTo(1);

        }

        private bool BeValidMatchResult(int matchResultId)
        {
            return MatchResult.List().Any(mr => mr.Id == matchResultId);
        }
    }
}
