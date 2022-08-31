using BettingApp.Services.Sportbook.API.Infrastructure.Exceptions;
using BettingApp.Services.Sportbook.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Model
{
    public class PossiblePick : IEntity
    {
        public string Id { get; private set; }

        public string MatchId { get; private set; }

        public MatchResult MatchResult { get; private set; }

        public int MatchResultId { get; private set; }

        public string MatchResultName { get; private set; }

        public decimal Odd { get; private set; }

        public decimal InitialOdd { get; private set; }

        public RequirementType RequirementType { get; private set; }

        public int RequirementTypeId { get; private set; }

        public string RequirementTypeName { get; private set; }

        public decimal RequiredValue { get; private set; }

        public bool IsCanceled { get; private set; }

        public bool IsDisabled { get; private set; }

        public bool IsBetable { get; private set; }


        protected PossiblePick()
        {
            Id = Guid.NewGuid().ToString();
            IsDisabled = false;
            IsCanceled = false;
        }

        public PossiblePick(string matchId, int matchResultId, decimal odd, 
                            int requirementTypeId, decimal requiredValue) 
            : this()
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
            MatchResultName = MatchResult.From(MatchResultId).Name;
            Odd = odd;
            InitialOdd = odd;
            RequirementTypeId = requirementTypeId;
            RequirementTypeName = RequirementType.From(RequirementTypeId).Name;
            RequiredValue = requiredValue;

            CalculateBetableStatus();
        }

        private void CalculateBetableStatus()
        {
            IsBetable = !IsCanceled && !IsDisabled;
        }

        public void UpdateOdd(decimal newOdd)
        {
            if (IsCanceled)
                throw new SportsbookDomainException("Cannot update a cancled possible pick.");

            Odd = newOdd;
        }

        public void Disable()
        {
            if (IsCanceled)
                throw new SportsbookDomainException("Cannot update a cancled possible pick.");

            IsDisabled = true;
            CalculateBetableStatus();
        }

        public void Enable()
        {
            if (IsCanceled)
                throw new SportsbookDomainException("Cannot update a cancled possible pick.");

            IsDisabled = false;
            CalculateBetableStatus();
        }

        // method that updates both Odd value and IsDisabled status of Possible Pick
        public void Update(decimal newOdd, bool isDisabled)
        {
            if (IsCanceled)
                throw new SportsbookDomainException("Cannot update a cancled possible pick.");

            Odd = newOdd;
            IsDisabled = isDisabled;
            CalculateBetableStatus();
        }

        public void Cancel()
        {
            if (IsCanceled)
                throw new SportsbookDomainException("Cannot cancel a possible pick that is already canceled.");

            IsCanceled = true;
            CalculateBetableStatus();
        }
    }
}
