using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class InvoiceLineRepo : IInvoiceLineRepo
    {

        private readonly AppDbContext _context;

        public InvoiceLineRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceLine> AddAsync(InvoiceLine invoiceLine)
        {
            _context.InvoiceLines.Add(invoiceLine);
            await _context.SaveChangesAsync();
            return invoiceLine;
        }

        public async Task<bool> DeleteAsync(InvoiceLine invoiceLine)
        {
            _context.InvoiceLines.Remove(invoiceLine);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<InvoiceLine>> GetAllAsync()
        {
            return await _context.InvoiceLines.ToListAsync();
        }

        public async Task<InvoiceLine> GetByIdAsync(int id)
        {
            return await _context.InvoiceLines.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(InvoiceLine invoiceLine)
        {
            _context.InvoiceLines.Update(invoiceLine);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
