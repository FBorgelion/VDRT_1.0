using AutoMapper;
using BL.DTOs.Invoice;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mappers
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile() 
        {

            CreateMap<Invoice, InvoiceDto>();

            CreateMap<CreateInvoiceDto, Invoice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateInvoiceDto, Invoice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceNumber, opt => opt.Ignore());
        }
    }
}
