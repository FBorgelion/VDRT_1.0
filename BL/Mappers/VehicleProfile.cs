using AutoMapper;
using BL.DTOs.Vehicle;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class VehicleProfile : Profile
    {

        public VehicleProfile() 
        {
            CreateMap<Vehicle, VehicleDto>();

            CreateMap<CreateVehicleDto, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateVehicleDto, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
