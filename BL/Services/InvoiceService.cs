using AutoMapper;
using BL.DTOs.Driver;
using BL.DTOs.Invoice;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class InvoiceService : IInvoiceService
    {

        protected readonly IMapper _mapper;
        protected readonly IInvoiceRepo _invoiceRepo;

        public InvoiceService(IMapper mapper, IInvoiceRepo invoiceRepo)
        {
            _mapper = mapper;
            _invoiceRepo = invoiceRepo;
        }

        public async Task<InvoiceDto> AddAsync(CreateInvoiceDto createInvoiceDto)
        {
            var invoice = _mapper.Map<Invoice>(createInvoiceDto);

            var createdInvoice = await _invoiceRepo.AddAsync(invoice);

            return _mapper.Map<InvoiceDto>(createdInvoice);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(id);
            if (invoice == null)
                return false;

            return await _invoiceRepo.DeleteAsync(invoice);
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _invoiceRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto?> GetByIdAsync(int id)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(id);

            if (invoice == null)
                return default;

            return _mapper.Map<InvoiceDto>(invoice);

        }

        public async Task<bool> UpdateAsync(int id, UpdateInvoiceDto updateInvoiceDto)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(id);

            if (invoice == null)
                return false;

            _mapper.Map(updateInvoiceDto, invoice);
            invoice.Id = id;

            return await _invoiceRepo.UpdateAsync(invoice);
        }
    }
}
