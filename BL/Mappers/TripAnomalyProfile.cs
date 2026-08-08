using AutoMapper;
using BL.DTOs.TripAnomaly;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class TripAnomalyProfile : Profile
    {

        public TripAnomalyProfile() 
        {

            CreateMap<TripAnomaly, TripAnomalyDto>();

            CreateMap<CreateTripAnomalyDto, TripAnomaly>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewerId, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewComments, opt => opt.Ignore());

            CreateMap<UpdateTripAnomalyDto, TripAnomaly>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MissionId, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityId, opt => opt.Ignore());

        }

    }
}
