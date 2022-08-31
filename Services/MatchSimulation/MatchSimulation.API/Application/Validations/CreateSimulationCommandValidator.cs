using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Validations
{
    public class CreateSimulationCommandValidator : AbstractValidator<CreateSimulationCommand>
    {
        public CreateSimulationCommandValidator()
        {
            RuleFor(command => command.MatchId).NotEmpty();
            RuleFor(command => command.HomeClub).SetValidator(new ClubValidator());
            RuleFor(command => command.AwayClub).SetValidator(new ClubValidator());
            RuleFor(command => command.LeagueId).Must(BeValidLeague)
                                                .WithMessage("LeagueId value is not valid.");
            RuleFor(command => command.KickoffDateTime).Must(BeLaterDateTime)
                                                       .WithMessage("KickoffDateTime is not later than current DateTime.");


        }

        private bool BeValidLeague(int leagueId)
        {
            return League.List().Any(l => l.Id == leagueId);
        }

        private bool BeLaterDateTime(DateTime dateTime)
        {
            return DateTime.Compare(DateTime.UtcNow.AddHours(2), dateTime) < 0;
        }
    }

    public class ClubValidator : AbstractValidator<Club>
    {
        public ClubValidator()
        {
            RuleFor(club => club.Id).NotEmpty();
            RuleFor(club => club.Name).NotEmpty();
            RuleFor(club => club.AttackStat).Must(BeValidStat)
                                            .WithMessage("Club's AttackStat value is not valid.");
            RuleFor(club => club.DefenceStat).Must(BeValidStat)
                                             .WithMessage("Club's DefenceStat value is not valid.");
            RuleFor(club => club.FormStat).Must(BeValidStat)
                                          .WithMessage("Club's FormStat value is not valid.");
        }

        private bool BeValidStat(int stat)
        {
            return stat >= 1 && stat <= 20;
        }
    }
}
