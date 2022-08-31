using BettingApp.Services.Betslips.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Validations
{
    public class RemoveSelectionCommandValidator : AbstractValidator<RemoveSelectionCommand>
    {
        public RemoveSelectionCommandValidator()
        {
            RuleFor(command => command.GamblerId).NotEmpty();
            RuleFor(command => command.SelectionId).NotEmpty();
        }
    }
}
