using BettingApp.Services.Betting.API.Application.Commands;
using BettingApp.Services.Betting.API.Application.DTOs;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Validations
{
    public class CreateBetCommandValidator : AbstractValidator<CreateBetCommand>
    {
        public CreateBetCommandValidator()
        {
            RuleFor(command => command.GamblerId).NotEmpty();
            RuleFor(command => command.WageredAmount).NotEmpty()
                                                     .GreaterThan(0).WithMessage("WaggeredAmount must be greater than zero.");
            RuleFor(command => command.SelectionDTOs).Must(selectionDtos => selectionDtos.Any()).WithMessage("No SelectionDTOs found.");
            RuleForEach(command => command.SelectionDTOs).SetValidator(new SelectionDTOValidator());
        }
        
    }

    public class SelectionDTOValidator : AbstractValidator<SelectionDTO>
    {
        public SelectionDTOValidator()
        {
            RuleFor(selectionDto => selectionDto.GamblerMatchResultId).NotEmpty()
                                                                .Must(BeValidMatchResult)
                                                                .WithMessage("SelectionDTO's GamblerMatchResultId value is not valid.");
            RuleFor(selectionDto => selectionDto.Odd).NotEmpty()
                                                .GreaterThan(1)
                                                .WithMessage("SelectionDTO odd must be grater than 1.");
            RuleFor(selectionDto => selectionDto.RelatedMatchId).NotEmpty();
            RuleFor(selectionDto => selectionDto.HomeClubName).NotEmpty();
            RuleFor(selectionDto => selectionDto.AwayClubName).NotEmpty();
            RuleFor(selectionDto => selectionDto.KickoffDateTime).NotEmpty();
            RuleFor(selectionDto => selectionDto.CurrentMinute).NotEmpty()
                                                         .Must(BeValidMinute)
                                                         .WithMessage("SelectionDTO Match's CurrentMinute value is not valid.");
            RuleFor(selectionDto => selectionDto.HomeClubScore).Must(BeValidScore)
                                                         .WithMessage("SelectionDTO Match's HomeClubScore value is not valid.");
            RuleFor(selectionDto => selectionDto.AwayClubScore).Must(BeValidScore)
                                                        .WithMessage("SelectionDTO Match's AwayClubScore value is not valid.");

        }

        private bool BeValidMatchResult(int matchResultId)
        {
            return MatchResult.List().Any(mr => mr.Id == matchResultId);
        }

        private bool BeValidMinute(string minute)
        {
            var regex = new Regex("^(((([4][5])|([9][0]))[+][1-9][0-9]*)|([1-8][0-9]?)|[0]|[9]|([9][0])|([H][T])|([F][T]))$");

            if (regex.IsMatch(minute)) return true;

            return false;
        }

        private bool BeValidScore(int score)
        {
            if (score >= 0) return true;

            return false;
        }
    }
}
