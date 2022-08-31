using BettingApp.Services.Betslips.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Validations
{
    public class UpdateWageredAmountCommandValidator : AbstractValidator<UpdateWageredAmountCommand>
    {
        public UpdateWageredAmountCommandValidator()
        {
            RuleFor(command => command.GamblerId).NotEmpty();
            RuleFor(command => command.NewWageredAmount).GreaterThanOrEqualTo(0);
        }
    }
}
