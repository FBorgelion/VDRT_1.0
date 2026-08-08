using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IInvoiceRepo
    {

        public Task<IEnumerable<Invoice>> GetAllAsync();
        public Task<Invoice> GetByIdAsync(int id);
        public Task<Invoice> AddAsync(Invoice invoice);
        public Task<bool> UpdateAsync(Invoice invoice);
        public Task<bool> DeleteAsync(Invoice invoice);

    }
}
