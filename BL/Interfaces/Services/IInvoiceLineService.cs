using BL.DTOs.Driver;
using BL.DTOs.InvoiceLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IInvoiceLineService
    {
        public Task<IEnumerable<InvoiceLineDto>> GetAllAsync();

        public Task<InvoiceLineDto?> GetByIdAsync(int id);

        public Task<InvoiceLineDto> AddAsync(CreateInvoiceLineDto createInvoiceLineDto);

        public Task<bool> UpdateAsync(int id, UpdateInvoiceLineDto updateInvoiceLineDto);

        Task<bool> DeleteAsync(int id);

    }
}
