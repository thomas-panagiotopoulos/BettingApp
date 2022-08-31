using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using BettingApp.Services.Betslips.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Infrastructure.Repositories
{
    public class BetslipRepository : IBetslipRepository
    {
        private readonly BetslipsContext _context;

        public BetslipRepository(BetslipsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public Betslip Add(Betslip betslip)
        {
            var entity = _context.Betslips.Add(betslip).Entity;

            return entity;
        }

        public void Update(Betslip betslip)
        {
            _context.Entry(betslip).State = EntityState.Modified;
        }

        public bool RemoveByGamblerId(string gamblerId)
        {
            var betslip = _context.Betslips.FirstOrDefault(b => b.GamblerId.Equals(gamblerId));

            if (betslip == null) return false;

            _context.Betslips.Remove(betslip);

            return true;
        }

        public bool RemoveSelectionById(string selectionId)
        {
            var selection = _context.Selections.FirstOrDefault(s => s.Id.Equals(selectionId));

            if (selection == null) return false;

            _context.Selections.Remove(selection);

            return true;
        }

        public bool ExistsWithGamblerId(string gamblerId)
        {
            return _context.Betslips.Any(b => b.GamblerId.Equals(gamblerId));
        }


        public async Task<Betslip> GetByIdAsync(string betslipId)
        {
            var betslip = await _context.Betslips
                                    .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match)
                                    .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                    .FirstOrDefaultAsync(b => b.Id.Equals(betslipId));

            // If betslip is not found in the DbSet, search in the LocalView for newly added betslips.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (betslip == null)
            {
                betslip = _context.Betslips
                                    .Local
                                    .AsQueryable()
                                    .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match)
                                    .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                    .FirstOrDefault(b => b.Id.Equals(betslipId));
            }

            return betslip;
        }


        public async Task<Betslip> GetByGamblerIdAsync(string gamblerId)
        {
            var betslip = await _context.Betslips
                                    .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match)
                                    .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                    .FirstOrDefaultAsync(b => b.GamblerId.Equals(gamblerId));

            // If betslip is not found the DbSet, search in the LocalView for newly added betslips.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (betslip == null)
            {
                betslip = _context.Betslips
                                    .Local
                                    .AsQueryable()
                                    .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match)
                                    .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                    .FirstOrDefault(b => b.GamblerId.Equals(gamblerId));
            }

            return betslip;
        }

        public async Task<Betslip> GetBySelectionIdAsync(string selectionId)
        {
            var betslip = await _context.Betslips
                                        .Where(b => b.Selections
                                                     .Where(s => s.Id.Equals(selectionId))
                                                     .Any())
                                        .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                        .Include(b => b.Selections).ThenInclude(s => s.Match)
                                        .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                        .FirstOrDefaultAsync();

            // If betslip is not found the DbSet, search in the LocalView for newly added betslips.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (betslip == null)
            {
                betslip = _context.Betslips
                                  .Local
                                  .AsQueryable()
                                  .Where(b => b.Selections
                                                     .Where(s => s.Id.Equals(selectionId))
                                                     .Any())
                                  .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                  .Include(b => b.Selections).ThenInclude(s => s.Match)
                                  .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                  .FirstOrDefault();
            }

            return betslip;

        }

        public async Task<Betslip> GetByMatchIdAsync(string matchId)
        {
            var betslip = await _context.Betslips
                                        .Where(b => b.Selections
                                                     .Where(s => s.Match.Id.Equals(matchId))
                                                     .Any())
                                        .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                        .Include(b => b.Selections).ThenInclude(s => s.Match)
                                        .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                        .FirstOrDefaultAsync();

            // If betslip is not found the DbSet, search in the LocalView for newly added betslips.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (betslip == null)
            {
                betslip = _context.Betslips
                                  .Local
                                  .AsQueryable()
                                  .Where(b => b.Selections
                                                .Where(s => s.Match.Id.Equals(matchId))
                                                .Any())
                                  .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                  .Include(b => b.Selections).ThenInclude(s => s.Match)
                                  .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                  .FirstOrDefault();
            }

            return betslip;
        }

        public async Task<Betslip> GetByRequirementIdAsync(string requirementId)
        {
            var betslip = await _context.Betslips
                                        .Where(b => b.Selections
                                                     .Where(s => s.Requirement.Id.Equals(requirementId))
                                                     .Any())
                                        .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                        .Include(b => b.Selections).ThenInclude(s => s.Match)
                                        .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                        .FirstOrDefaultAsync();

            // If betslip is not found the DbSet, search in the LocalView for newly added betslips.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (betslip == null)
            {
                betslip = _context.Betslips
                                  .Local
                                  .AsQueryable()
                                  .Where(b => b.Selections
                                                .Where(s => s.Requirement.Id.Equals(requirementId))
                                                .Any())
                                  .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                  .Include(b => b.Selections).ThenInclude(s => s.Match)
                                  .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                  .FirstOrDefault();
            }

            return betslip;
        }

        public IEnumerable<Betslip> GetBetslipsWithRelatedMatch(string relatedMatchId)
        {
            var betslips = _context.Betslips
                                   .Where(b => b.Selections
                                                .Any(s => s.Match.RelatedMatchId.Equals(relatedMatchId)))
                                   .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                   .Include(b => b.Selections).ThenInclude(s => s.Match)
                                   .Include(b => b.Selections).ThenInclude(s => s.Requirement).ThenInclude(r => r.RequirementType)
                                   .ToList();

            return betslips;
        }

    }
}
