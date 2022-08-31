using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class AddSelectionCommand : IRequest<Betslip>
    {
        [DataMember]
        public string GamblerId { get; private set; }

        [DataMember]
        public int GamblerMatchResultId { get; private set; }

        [DataMember]
        public decimal Odd { get; private set; }

        [DataMember]
        public decimal InitialOdd { get; private set; }

        [DataMember]
        public string RelatedMatchId { get; private set; }

        [DataMember]
        public string HomeClubName { get; private set; }

        [DataMember]
        public string AwayClubName { get; private set; }

        [DataMember]
        public DateTime KickoffDateTime { get; private set; }

        [DataMember]
        public string CurrentMinute { get; private set; }

        [DataMember]
        public int HomeClubScore { get; private set; }

        [DataMember]
        public int AwayClubScore { get; private set; }

        [DataMember]
        public int RequirementTypeId { get; private set; }

        [DataMember]
        public decimal RequiredValue { get; private set; }

        [DataMember]
        public string LatestAdditionId { get; private set; }
        public AddSelectionCommand(string gamblerId, int gamblerMatchResultId, decimal odd, decimal initialOdd, string relatedMatchId,
            string homeClubName, string awayClubName, DateTime kickoffDateTime, string currentMinute,
            int homeClubScore, int awayClubScore, int requirementTypeId, decimal requiredValue, string latestAdditionId)
        {
            GamblerId = gamblerId;
            GamblerMatchResultId = gamblerMatchResultId;
            Odd = odd;
            InitialOdd = initialOdd;
            RelatedMatchId = relatedMatchId;
            HomeClubName = homeClubName;
            AwayClubName = awayClubName;
            KickoffDateTime = kickoffDateTime;
            CurrentMinute = currentMinute;
            HomeClubScore = homeClubScore;
            AwayClubScore = awayClubScore;
            RequirementTypeId = requirementTypeId;
            RequiredValue = requiredValue;
            LatestAdditionId = latestAdditionId;
        }
    }
}
