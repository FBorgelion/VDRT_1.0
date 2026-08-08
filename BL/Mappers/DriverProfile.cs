using AutoMapper;
using BL.DTOs.Driver;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class DriverProfile : Profile
    {

        public DriverProfile() 
        {

            CreateMap<Driver, DriverDto>();

            CreateMap<CreateDriverDto, Driver>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateDriverDto, Driver>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
