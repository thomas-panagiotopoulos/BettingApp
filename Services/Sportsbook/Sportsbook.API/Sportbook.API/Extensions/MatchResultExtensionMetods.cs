using BettingApp.Services.Sportbook.API.DTOs;
using BettingApp.Services.Sportbook.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Extensions
{
    public static class MatchResultExtensionMetods
    {
        public static MatchResultDTO ToMatchResultDTO(this MatchResult matchResult)
        {
            return new MatchResultDTO()
            {
                Id = matchResult.Id,
                Name = matchResult.Name,
                AliasName = matchResult.AliasName,
            };
        }
    }
}
