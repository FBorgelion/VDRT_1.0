using BL.DTOs.Driver;
using BL.DTOs.Invoice;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IInvoiceService
    {

        public Task<IEnumerable<InvoiceDto>> GetAllAsync();

        public Task<InvoiceDto?> GetByIdAsync(int id);

        public Task<InvoiceDto> AddAsync(CreateInvoiceDto createInvoiceDto);

        public Task<bool> UpdateAsync(int id, UpdateInvoiceDto updateInvoiceDto);

        Task<bool> DeleteAsync(int id);

    }
}
