using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class VehicleAlertRepo : IVehicleAlertRepo
    {

        private readonly AppDbContext _context;

        public VehicleAlertRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleAlert> AddAsync(VehicleAlert alert)
        {
            _context.VehicleAlerts.Add(alert);
            await _context.SaveChangesAsync();
            return alert;
        }

        public async Task<bool> DeleteAsync(VehicleAlert alert)
        {
            _context.VehicleAlerts.Remove(alert);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<VehicleAlert>> GetAllAsync()
        {
           return await _context.VehicleAlerts.ToListAsync();
        }

        public async Task<VehicleAlert> GetByIdAsync(int id)
        {
            return await _context.VehicleAlerts.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(VehicleAlert alert)
        {
            _context.VehicleAlerts.Update(alert);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
