using BettingApp.Services.Betslips.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Validations
{
    public class UpdateMatchCurrentMinuteCommandValidator : AbstractValidator<UpdateMatchCurrentMinuteCommand>
    {
        public UpdateMatchCurrentMinuteCommandValidator()
        {
            RuleFor(command => command.MatchId).NotEmpty();
            RuleFor(command => command.NewCurrentMinute).NotEmpty()
                                                        .Must(BeValidMinute)
                                                        .WithMessage("CurrentMinute value is not valid.");

        }

        private bool BeValidMinute(string minute)
        {
            var regex = new Regex("^(((([4][5])|([9][0]))[+][1-9][0-9]*)|([1-8][0-9]?)|[0]|[9]|([9][0])|([H][T])|([F][T]))$");

            if (regex.IsMatch(minute)) return true;

            return false;
        }
    }
}
