using AutoMapper;
using BL.DTOs.Mission;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class MissionProfile : Profile
    {

        public MissionProfile() 
        {
            CreateMap<Mission, MissionDto>();

            CreateMap<CreateMissionDto, Mission>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Site, opt => opt.Ignore())
            .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
            .ForMember(dest => dest.Driver, opt => opt.Ignore())
            .ForMember(dest => dest.Activities, opt => opt.Ignore())
            .ForMember(dest => dest.TripAnomalies, opt => opt.Ignore());

            CreateMap<UpdateMissionDto, Mission>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Site, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.TripAnomalies, opt => opt.Ignore());
        }

    }
}
