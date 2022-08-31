using BettingApp.Services.Betting.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Validations
{
    public class UpdateMatchScoresCommandValidator : AbstractValidator<UpdateMatchScoresCommand>
    {
        public UpdateMatchScoresCommandValidator()
        {
            RuleFor(command => command.MatchId).NotEmpty();
            RuleFor(command => command.NewHomeClubScore).Must(BeValidScore)
                                                        .WithMessage("NewHomeClubScore value is not valid.");
            RuleFor(command => command.NewAwayClubScore).Must(BeValidScore)
                                                        .WithMessage("NewAwayClubScore value is not valid.");
        }

        private bool BeValidScore(int score)
        {
            if (score >= 0) return true;

            return false;
        }
    }
}
