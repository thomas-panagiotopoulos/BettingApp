using BettingApp.Services.Betslips.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate
{
    public interface IBetslipRepository : IRepository<Betslip>
    {
        Betslip Add(Betslip betslip);

        void Update(Betslip betslip);

        bool RemoveByGamblerId(string gamblerId);

        bool RemoveSelectionById(string selectionId);

        bool ExistsWithGamblerId(string gamblerId);

        Task<Betslip> GetByIdAsync(string betslipId);

        Task<Betslip> GetByGamblerIdAsync(string gamblerId);

        Task<Betslip> GetBySelectionIdAsync(string selectionId);

        Task<Betslip> GetByMatchIdAsync(string matchId);

        Task<Betslip> GetByRequirementIdAsync(string requirementId);

        IEnumerable<Betslip> GetBetslipsWithRelatedMatch(string relatedMatchId);
    }
}
