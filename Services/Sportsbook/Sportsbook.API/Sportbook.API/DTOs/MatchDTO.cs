using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.DTOs
{
    public class MatchDTO
    {
        public string MatchId { get; set; }

        public string HomeClubName { get; set; }

        public string AwayClubName { get; set; }

        public int LeagueId { get; set; }

        public string LeagueName { get; set; }

        public DateTime KickoffDateTime { get; set; }

        public string CurrentMinute { get; set; }

        public int HomeClubScore { get; set; }

        public int AwayClubScore { get; set; }

        public bool IsCanceled { get; set; }

        public List<PossiblePickDTO> PossiblePickDTOs { get; set; }
    }
}
