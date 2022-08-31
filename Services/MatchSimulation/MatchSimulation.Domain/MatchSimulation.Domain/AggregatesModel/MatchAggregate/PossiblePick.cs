using BettingApp.Services.MatchSimulation.Domain.Events;
using BettingApp.Services.MatchSimulation.Domain.Exceptions;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate
{
    public class PossiblePick : Entity
    {
        // Id

        public string MatchId => _matchId;
        private string _matchId;

        public MatchResult MatchResult { get; private set; }

        public int MatchResultId => _matchResultId;
        private int _matchResultId;

        public string MatchResultName => _matchResultName;
        private string _matchResultName;

        public decimal Odd => _odd;
        private decimal _odd;

        public RequirementType RequirementType { get; private set; }

        public int RequirementTypeId => _requirementTypeId;
        private int _requirementTypeId;

        public string RequirementTypeName => _requirementTypeName;
        private string _requirementTypeName;

        public decimal RequiredValue => _requiredValue;
        private decimal _requiredValue;

        public bool IsCanceled => _isCanceled;
        private bool _isCanceled;

        public bool IsDisabled => _isDisabled;
        public bool _isDisabled;



        protected PossiblePick()
        {
            Id = Guid.NewGuid().ToString();
            _isDisabled = false;
            _isCanceled = false;
        }

        public PossiblePick(string matchId, int matchResultId, decimal odd,
                            int requirementTypeId, decimal requiredValue)
            : this()
        {
            _matchId = matchId;
            _matchResultId = MatchResult.From(matchResultId).Id;
            _matchResultName = MatchResult.From(MatchResultId).Name;
            _odd = odd;
            _requirementTypeId = RequirementType.From(requirementTypeId).Id;
            _requirementTypeName = RequirementType.From(RequirementTypeId).Name;
            _requiredValue = requiredValue;
        }

        public void UpdateOdd(decimal newOdd)
        {
            if (IsCanceled)
                throw new MatchSimulationDomainException("Cannot update a cancled possible pick.");

            var oldOdd = _odd;
            _odd = newOdd;
            if (_odd != oldOdd)
            {
                // Add a PossiblePickOddChangedDomainEvent
                AddDomainEvent(new PossiblePickOddChangedDomainEvent(_matchId, _matchResultId, _odd));
            }
        }

        public void Disable()
        {
            if (IsCanceled)
                throw new MatchSimulationDomainException("Cannot update a cancled possible pick.");

            var oldIsDisabled = _isDisabled;
            _isDisabled = true;

            if (oldIsDisabled != _isDisabled)
            {
                // Add a PossiblePickDisabledDomainEvent
                AddDomainEvent(new PossiblePickDisabledDomainEvent(_matchId, _matchResultId));
            }
        }

        public void Enable()
        {
            if (IsCanceled)
                throw new MatchSimulationDomainException("Cannot update a cancled possible pick.");

            var oldIsDisabled = _isDisabled;
            _isDisabled = false;
            
            if(oldIsDisabled != _isDisabled)
            {
                // Add a PossiblePickEnabledDomainEvent
                AddDomainEvent(new PossiblePickEnabledDomainEvent(_matchId, _matchResultId));
            }
        }

        // method that updates both Odd value and IsDisabled status of Possible Pick
        public void Update(decimal newOdd, bool isDisabled)
        {
            if (IsCanceled)
                throw new MatchSimulationDomainException("Cannot update a cancled possible pick.");

            var oldOdd = _odd;
            var oldIsDisabled = _isDisabled;

            _odd = newOdd;
            _isDisabled = isDisabled;

            if (_odd != oldOdd || _isDisabled != oldIsDisabled)
            {
                // Add a PossiblePickOddOrStatusChangedDomainEvent
                AddDomainEvent(new PossiblePickOddOrStatusChangedDomainEvent(_matchId, _matchResultId, _odd, _isDisabled));
            }
        }

        public void Cancel()
        {
            if (IsCanceled)
                throw new MatchSimulationDomainException("Cannot cancel a possible pick that is already canceled.");

            _isCanceled = true;   
        }

    }
}
