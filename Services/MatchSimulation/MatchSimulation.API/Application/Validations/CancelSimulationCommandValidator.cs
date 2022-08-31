using BettingApp.Services.MatchSimulation.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Validations
{
    public class CancelSimulationCommandValidator : AbstractValidator<CancelSimulationCommand>
    {
        public CancelSimulationCommandValidator()
        {
            RuleFor(command => command.SimulationId).NotEmpty();
        }
    }
}
