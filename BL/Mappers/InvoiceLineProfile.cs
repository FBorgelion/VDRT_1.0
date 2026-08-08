using AutoMapper;
using BL.DTOs.InvoiceLine;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class InvoiceLineProfile : Profile
    {

        public InvoiceLineProfile()
        {
            CreateMap<InvoiceLine, InvoiceLineDto>();

            CreateMap<CreateInvoiceLineDto, InvoiceLine>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateInvoiceLineDto, InvoiceLine>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
