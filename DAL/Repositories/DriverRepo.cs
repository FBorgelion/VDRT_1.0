using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class DriverRepo : IDriverRepo
    {

        private readonly AppDbContext _context;

        public DriverRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Driver> AddAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return driver;
        }

        public async Task<bool> DeleteAsync(Driver driver)
        {
            _context.Drivers.Remove(driver);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver> GetByIdAsync(int id)
        {
            return await _context.Drivers.FindAsync(id); //retourne null si pas trouvé, sinon retourne le driver trouvé. Gestion dans le service
        }

        public async Task<bool> UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
