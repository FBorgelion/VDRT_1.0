using AutoMapper;
using BL.DTOs.Position;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class PositionProfile : Profile
    {
        public PositionProfile() 
        {
            CreateMap<Position, PositionDto>();

            CreateMap<CreatePositionDto, Position>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdatePositionDto, Position>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.VehicleId, opt => opt.Ignore());
        }
    }
}
