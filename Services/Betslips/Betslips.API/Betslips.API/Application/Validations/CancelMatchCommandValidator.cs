using BettingApp.Services.Betslips.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Validations
{
    public class CancelMatchCommandValidator : AbstractValidator<CancelMatchCommand>
    {
        public CancelMatchCommandValidator()
        {
            RuleFor(command => command.MatchId).NotEmpty();
        }
    }
}
