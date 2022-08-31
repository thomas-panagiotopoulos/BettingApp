using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.DTOs
{
    public class SelectionDTO
    {
        public string Id { get; set; }

        public int GamblerMatchResultId { get; set; }

        public string GamblerMatchResultName { get; set; }

        public string GamblerMatchResultAliasName { get; set; }

        public string SelectionTypeName { get; set; }

        public decimal Odd { get; set; }

        public string RelatedMatchId { get; set; }

        public string HomeClubName { get; set; }

        public string AwayClubName { get; set; }

        public DateTime KickoffDateTime { get; set; }

        public string CurrentMinute { get; set; }

        public int HomeClubScore { get; set; }

        public int AwayClubScore { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool IsCanceled { get; set; }

        public int ResultId { get; set; }
        public string ResultName { get; set; }
    }
}
