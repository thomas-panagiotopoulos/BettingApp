using AutoMapper;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.AutoMapper.Profiles
{
    public class BetProfile : Profile
    {
        public BetProfile()
        {
            CreateMap<BetDTO, Bet>()
                .ForMember(dest =>
                    dest.Selections,
                    opt => opt.MapFrom(src => src.SelectionDTOs));
        }
    }
}
