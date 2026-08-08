using AutoMapper;
using BL.DTOs.Site;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class SiteProfile : Profile
    {

        public SiteProfile() 
        {
            CreateMap<Sites, SiteDto>();

            CreateMap<CreateSiteDto, Sites>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateSiteDto, Sites>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
