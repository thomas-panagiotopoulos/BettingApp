using BettingApp.Services.Betslips.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Validations
{
    public class UpdateWalletBalanceCommandValidator : AbstractValidator<UpdateWalletBalanceCommand>
    {
        public UpdateWalletBalanceCommandValidator()
        {
            RuleFor(command => command.GamblerId).NotEmpty();
            RuleFor(command => command.NewBalance).GreaterThanOrEqualTo(0);
        }
    }
}
