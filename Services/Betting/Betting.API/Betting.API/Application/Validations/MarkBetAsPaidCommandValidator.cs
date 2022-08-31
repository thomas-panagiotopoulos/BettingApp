using BettingApp.Services.Betting.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Validations
{
    public class MarkBetAsPaidCommandValidator : AbstractValidator<MarkBetAsPaidCommand>
    {
        public MarkBetAsPaidCommandValidator()
        {
            RuleFor(command => command.BetId).NotEmpty();
        }
    }
}
