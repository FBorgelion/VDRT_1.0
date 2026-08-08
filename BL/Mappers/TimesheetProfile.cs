using AutoMapper;
using BL.DTOs.Timesheet;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class TimesheetProfile : Profile
    {
        public TimesheetProfile()
        {
            CreateMap<Timesheet, TimesheetDto>();

            CreateMap<CreateTimesheetDto, Timesheet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApproverId, opt => opt.Ignore());

            CreateMap<UpdateTimesheetDto, Timesheet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApproverId, opt => opt.Ignore())
                .ForMember(dest => dest.DriverId, opt => opt.Ignore());
        }
    }
}
