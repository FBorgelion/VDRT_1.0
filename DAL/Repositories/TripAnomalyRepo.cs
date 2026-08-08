using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class TripAnomalyRepo : ITripAnomalyRepo
    {

        private readonly AppDbContext _context;

        public TripAnomalyRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TripAnomaly> AddAsync(TripAnomaly tripAnomaly)
        {
            _context.TripAnomalies.Add(tripAnomaly);
            await _context.SaveChangesAsync();

            return tripAnomaly;
        }

        public async Task<bool> DeleteAsync(TripAnomaly tripAnomaly)
        {
            _context.TripAnomalies.Remove(tripAnomaly);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TripAnomaly>> GetAllAsync()
        {
            return await _context.TripAnomalies.ToListAsync();
        }

        public async Task<TripAnomaly> GetByIdAsync(int id)
        {
            return await _context.TripAnomalies.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(TripAnomaly tripAnomaly)
        {
            _context.TripAnomalies.Update(tripAnomaly);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
