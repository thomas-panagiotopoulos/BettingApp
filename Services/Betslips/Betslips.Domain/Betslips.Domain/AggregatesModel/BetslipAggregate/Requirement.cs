using BettingApp.Services.Betslips.Domain.Events;
using BettingApp.Services.Betslips.Domain.Exceptions;
using BettingApp.Services.Betslips.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate
{
    public class Requirement : Entity
    {
        // Id

        public string SelectionId => _selectionId;
        private string _selectionId;

        public string RelatedMatchId => _relatedMatchId;
        private string _relatedMatchId;

        public int SelectionTypeId => _selectionTypeId;
        private int _selectionTypeId;

        public RequirementType RequirementType { get; private set; }
        public int RequirementTypeId => _requirementTypeId;
        private int _requirementTypeId;
        public string RequirementTypeName => _requirementTypeName;
        private string _requirementTypeName;

        public decimal RequiredValue => _requiredValue;
        private decimal _requiredValue; // declare as decimal to cover both Selections.Count and WageredAmount

        public bool IsFulfilled => _isFulfilled;
        private bool _isFulfilled;


        // constructors

        protected Requirement()
        {
            _isFulfilled = false;
        }

        public Requirement(string selectionId, string relatedMatchId, int selectionTypeId, 
                            int requirementTypeId, decimal requiredValue)
            : this()
        {
            _selectionId = selectionId;
            Id = CalculateUniqueIdForRequirement();

            _relatedMatchId = relatedMatchId;
            _selectionTypeId = selectionTypeId;

            _requirementTypeId = requirementTypeId;
            _requirementTypeName = RequirementType.From(requirementTypeId).Name;

            if(requirementTypeId == RequirementType.NoRequirement.Id)
            {
                _requiredValue = 0;
                _isFulfilled = true;
            }
            else
            {
                _requiredValue = requiredValue;
            }

            // We calculate fulfillment status of the Requirement after the Addition of the Selection
            // to the Betslip, because this newly added selection must be included in fulfillment calculation.
            // e.g. If Betslip curently has 2 selections and we try to add a 3rd selection
            // with MinimumSelections = 3, then when fulfillment calculation takes place it will give "false",
            // instead of "true", because it will only count 2 selections because the "in-progress" addition 
            // of the 3rd selection hasn't been completed yet.    
        }

        public void CalculateFulfillment(Betslip parentBetslip)
        {
            bool isFulfilledOld = _isFulfilled;

            switch (_requirementTypeId)
            {
                case var RequirementTypeId when RequirementTypeId == RequirementType.NoRequirement.Id:
                    _isFulfilled = true;
                    break;

                case var RequirementTypeId when RequirementTypeId == RequirementType.MinimunSelections.Id:
                    _isFulfilled = parentBetslip.Selections.Count >= _requiredValue;
                    break;

                case var RequirementTypeId when RequirementTypeId == RequirementType.MinimumWageredAmount.Id:
                    _isFulfilled = parentBetslip.WageredAmount >= _requiredValue;
                    break;

                case var RequirementTypeId when RequirementTypeId == RequirementType.MaximumSelections.Id:
                    _isFulfilled = parentBetslip.Selections.Count <= _requiredValue;
                    break;

                case var RequirementTypeId when RequirementTypeId == RequirementType.MaximumWageredAmount.Id:
                    _isFulfilled = parentBetslip.WageredAmount <= _requiredValue;
                    break;

                default:
                    throw new BetslipsDomainException("Requirement.CheckIfRequirementIsFulfilled: BetslipRule TypeId is not valid.");
            }

            if (_isFulfilled != isFulfilledOld)
            {
                // Add a RequirementFulfillmentStatusChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new RequirementFulfillmentStatusChangedDomainEvent(this.Id));
            }
        }

        // class method that claculates a unique Id for the Requirement, based on the Selections's Id
        // and Requirement's creation datetime
        private string CalculateUniqueIdForRequirement()
        {
            // first create a concatenated string (64 chars) which consists from
            // Selection's ID MD5 hash (32 chars), Requirement's creation date (18 chars)
            // and a random string (14 chars)
            var randStr = RandomString(14);
            var datetime = DateTime.UtcNow;
            var selectionIdMD5Hash = CalculateMD5HashForString(_selectionId);
            var concatStr = selectionIdMD5Hash +
                            datetime.ToString("yyyyMMddHHmmssffff") +
                            randStr;

            // then calculate the SHA256 hash of the concatenated string to make it "prettier"
            // we use SHA256 to "ensure" uniqueness for the produced Id
            var uniqueRequirementId = CalculateSHA256HashForString(concatStr);

            // we finally return the unique Requirement Id
            return uniqueRequirementId;
        }

        // helper method for CalculateUniqueIdForMatch() that calculates SHA256 hash for provided string
        private static string CalculateSHA256HashForString(string theString)
        {
            string hash;

            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                hash = BitConverter.ToString(
                  sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(theString))
                ).Replace("-", String.Empty);
            }

            return hash;
        }


        // helper method for CalculateUniqueIdForMatch() that calculates MD5 hash for provided string
        private static string CalculateMD5HashForString(string theString)
        {
            string hash;
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                  md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(theString))
                ).Replace("-", String.Empty);
            }
            return hash;
        }

        // helper method for CalculateUniqueIdForMatch() that creates a random string with provided length
        private static string RandomString(int length)
        {
            Random random = new Random();

            string chars = "abcdefghijklmnopqrstuvwxyz" +
                           "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                           "0123456789 ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}

