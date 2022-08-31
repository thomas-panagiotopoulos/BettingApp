using BettingApp.Services.Sportbook.API.Infrastructure.Exceptions;
using BettingApp.Services.Sportbook.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Model
{
    public class RequirementType : IEnumeration
    {
        public static RequirementType NoRequirement = new RequirementType(1, nameof(NoRequirement).ToLowerInvariant(), "No Req");
        public static RequirementType MinimunSelections = new RequirementType(2, nameof(MinimunSelections).ToLowerInvariant(), "Min Sel");
        public static RequirementType MinimumWageredAmount = new RequirementType(3, nameof(MinimumWageredAmount).ToLowerInvariant(), "Min Wag");
        public static RequirementType MaximumSelections = new RequirementType(4, nameof(MaximumSelections).ToLowerInvariant(), "Max Sel");
        public static RequirementType MaximumWageredAmount = new RequirementType(5, nameof(MaximumWageredAmount).ToLowerInvariant(), "Max Wag");

        public int Id { get; set; }
        public string Name { get; set; }
        public string AliasName { get; set; }

        public RequirementType(int id, string name, string aliasName)
        {
            Id = id;
            Name = name;
            AliasName = aliasName;
        }

        public static IEnumerable<RequirementType> List() =>
            new[] { NoRequirement, MinimunSelections, MinimumWageredAmount, MaximumSelections, MaximumWageredAmount };

        public static RequirementType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new SportsbookDomainException($"Possible values for RequirementType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static RequirementType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new SportsbookDomainException($"Possible values for RequirementType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
