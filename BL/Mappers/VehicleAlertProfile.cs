using AutoMapper;
using BL.DTOs.VehicleAlert;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class VehicleAlertProfile : Profile
    {
        public VehicleAlertProfile() 
        {
            CreateMap<VehicleAlert, VehicleAlertDto>();

            CreateMap<CreateVehicleAlertDto, VehicleAlert>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.VehicleId, opt => opt.Ignore());

            CreateMap<UpdateVehicleAlertDto, VehicleAlert>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.VehicleId, opt => opt.Ignore());
        }
    }
}
