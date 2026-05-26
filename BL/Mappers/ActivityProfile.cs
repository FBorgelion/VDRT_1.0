using AutoMapper;
using BL.DTOs.Activity;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityDto>();

            CreateMap<CreateActivityDto, Activity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateActivityDto, Activity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
