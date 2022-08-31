using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.DTOs
{
    public class BetslipDTO
    {
        public string GamblerId { get; set; }
        public decimal WageredAmount { get; set; }
        public List<SelectionDTO> SelectionDTOs { get; set; }
    }
}
