using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class InvoiceRepo : IInvoiceRepo
    {

        private readonly AppDbContext _context;

        public InvoiceRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<bool> DeleteAsync(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoices.FindAsync(id); //retourne null si pas trouvé, sinon retourne le driver trouvé. Gestion dans le service
        }

        public async Task<bool> UpdateAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
