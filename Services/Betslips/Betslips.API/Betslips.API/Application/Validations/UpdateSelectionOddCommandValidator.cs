using BettingApp.Services.Betslips.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Validations
{
    public class UpdateSelectionOddCommandValidator : AbstractValidator<UpdateSelectionOddCommand>
    {
        public UpdateSelectionOddCommandValidator()
        {
            RuleFor(command => command.SelectionId).NotEmpty();
            RuleFor(command => command.NewOdd).GreaterThanOrEqualTo(1);
        }
    }
}
