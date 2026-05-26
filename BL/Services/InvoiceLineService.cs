using AutoMapper;
using BL.DTOs.Driver;
using BL.DTOs.InvoiceLine;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class InvoiceLineService : IInvoiceLineService
    {

        protected readonly IInvoiceLineRepo _invoiceLineRepo;
        protected readonly IMapper _mapper;

        public InvoiceLineService(IInvoiceLineRepo invoiceLineRepo, IMapper mapper)
        {
            _invoiceLineRepo = invoiceLineRepo;
            _mapper = mapper;
        }

        public async Task<InvoiceLineDto> AddAsync(CreateInvoiceLineDto createInvoiceLineDto)
        {
            var line = _mapper.Map<InvoiceLine>(createInvoiceLineDto);

            var createdLine= await _invoiceLineRepo.AddAsync(line);

            return _mapper.Map<InvoiceLineDto>(createdLine);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var line = await _invoiceLineRepo.GetByIdAsync(id);
            if (line == null)
                return false;

            return await _invoiceLineRepo.DeleteAsync(line);
        }

        public async Task<IEnumerable<InvoiceLineDto>> GetAllAsync()
        {
            var lines = await _invoiceLineRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceLineDto>>(lines);
        }

        public async Task<InvoiceLineDto?> GetByIdAsync(int id)
        {
            var line = await _invoiceLineRepo.GetByIdAsync(id);

            if (line == null)
                return default;

            return _mapper.Map<InvoiceLineDto>(line);

        }

        public async Task<bool> UpdateAsync(int id, UpdateInvoiceLineDto updateInvoiceLineDto)
        {
            var line = await _invoiceLineRepo.GetByIdAsync(id);

            if (line == null)
                return false;

            _mapper.Map(updateInvoiceLineDto, line);
            line.Id = id;

            return await _invoiceLineRepo.UpdateAsync(line);
        }

    }
}
