using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class SiteRepo : ISiteRepo
    {

        private readonly AppDbContext _context;

        public SiteRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Sites> AddAsync(Sites site)
        {
            _context.Sites.Add(site);
            await _context.SaveChangesAsync();

            return site;
        }

        public async Task<bool> DeleteAsync(Sites site)
        {
            _context.Sites.Remove(site);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Sites>> GetAllAsync()
        {
            return await _context.Sites.ToListAsync();
        }

        public async Task<Sites> GetByIdAsync(int id)
        {
            return await _context.Sites.FindAsync(id); //retourne null si pas trouvé, sinon retourne le driver trouvé. Gestion dans le service
        }

        public async Task<bool> UpdateAsync(Sites site)
        {
            _context.Sites.Update(site);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
