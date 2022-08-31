using BettingApp.Services.MatchSimulation.Domain.Exceptions;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate
{
    public class RequirementType : Enumeration
    {
        public static RequirementType NoRequirement = new RequirementType(1, nameof(NoRequirement).ToLowerInvariant());
        public static RequirementType MinimumSelections = new RequirementType(2, nameof(MinimumSelections).ToLowerInvariant());
        public static RequirementType MinimumWageredAmount = new RequirementType(3, nameof(MinimumWageredAmount).ToLowerInvariant());
        public static RequirementType MaximumSelections = new RequirementType(4, nameof(MaximumSelections).ToLowerInvariant());
        public static RequirementType MaximumWageredAmount = new RequirementType(5, nameof(MaximumWageredAmount).ToLowerInvariant());

        public RequirementType(int id, string name) : base(id,name)
        {
        }

        public static IEnumerable<RequirementType> List() =>
            new[] { NoRequirement, MinimumSelections, MinimumWageredAmount, MaximumSelections, MaximumWageredAmount };

        public static RequirementType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new MatchSimulationDomainException($"Possible values for RequirementType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static RequirementType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new MatchSimulationDomainException($"Possible values for RequirementType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
