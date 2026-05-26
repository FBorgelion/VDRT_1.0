using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class PositionRepo : IPositionRepo
    {
        
        private readonly AppDbContext _context;

        public PositionRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Position> AddAsync(Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            return position;
        }

        public async Task<bool> DeleteAsync(Position position)
        {
            _context.Positions.Remove(position);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            return await _context.Positions.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Position position)
        {
            _context.Positions.Update(position);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
