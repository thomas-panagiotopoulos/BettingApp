using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using BettingApp.Services.Betting.API.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class CreateBetCommand : IRequest<Bet>
    {
        [DataMember]
        public string BetId { get; private set; }

        [DataMember]
        public string GamblerId { get; private set; }

        [DataMember]
        public decimal WageredAmount { get; private set; }

        [DataMember]
        private readonly List<SelectionDTO> _selectionDtos;

        [DataMember]
        public IEnumerable<SelectionDTO> SelectionDTOs => _selectionDtos;


        protected CreateBetCommand()
        {
            _selectionDtos = new List<SelectionDTO>();
        }

        public CreateBetCommand(string betId, string gamblerId, decimal wageredAmount, List<SelectionDTO> selections)
            : this()
        {
            BetId = betId;
            GamblerId = gamblerId;
            WageredAmount = wageredAmount;
            _selectionDtos = selections;
        }

    }
}
