using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Models
{
    public class Match
    {
        public string Id { get; set; }

        public string HomeClubName { get; set; }

        public string AwayClubName { get; set; }

        public int LeagueId { get; set; }

        public string LeagueName { get; set; }

        public DateTime KickoffDateTime { get; set; }

        public string CurrentMinute { get; set; }

        public int HomeClubScore { get; set; }

        public int AwayClubScore { get; set; }

        public bool IsCanceled { get; set; }

        public List<PossiblePick> PossiblePicks { get; set; }

        public PossiblePick GetPossiblePick(int matchResultId)
        {
            return PossiblePicks.SingleOrDefault(p => p.MatchResultId == matchResultId);
        }
    }
}
