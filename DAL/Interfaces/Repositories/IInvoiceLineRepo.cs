using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IInvoiceLineRepo
    {

        public Task<IEnumerable<InvoiceLine>> GetAllAsync();
        public Task<InvoiceLine> GetByIdAsync(int id);
        public Task<InvoiceLine> AddAsync(InvoiceLine invoiceLine);
        public Task<bool> UpdateAsync(InvoiceLine invoiceLine);
        public Task<bool> DeleteAsync(InvoiceLine invoiceLine);

    }
}
