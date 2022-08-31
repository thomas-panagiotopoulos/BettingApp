using BettingApp.Services.MatchSimulation.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Validations
{
    public class UpdateMatchScoresCommandValidator : AbstractValidator<UpdateMatchScoresCommand>
    {
        public UpdateMatchScoresCommandValidator()
        {
            RuleFor(command => command.MatchId).NotEmpty();
            RuleFor(command => command.NewHomeClubScore).GreaterThanOrEqualTo(0);
            RuleFor(command => command.NewAwayClubScore).GreaterThanOrEqualTo(0);
        }
    }
}
