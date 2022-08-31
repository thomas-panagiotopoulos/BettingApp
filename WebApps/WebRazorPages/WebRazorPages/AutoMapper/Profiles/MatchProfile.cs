using AutoMapper;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.AutoMapper.Profiles
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<MatchDTO, Match>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.MatchId))
                .ForMember(dest =>
                    dest.PossiblePicks,
                    opt => opt.MapFrom(src => src.PossiblePickDTOs));

            CreateMap<List<MatchDTO>, List<Match>>();
        }
    }
}
