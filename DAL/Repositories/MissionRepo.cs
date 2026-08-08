using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class MissionRepo : IMissionRepo
    {

        private readonly AppDbContext _context;

        public MissionRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Mission> AddAsync(Mission mission)
        {
            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            return mission;
        }

        public async Task<bool> DeleteAsync(Mission mission)
        {
            _context.Missions.Remove(mission);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Mission>> GetAllAsync()
        {
            return await _context.Missions.ToListAsync();
        }

        public async Task<Mission> GetByIdAsync(int id)
        {
            return await _context.Missions.FindAsync(id); //retourne null si pas trouvé, sinon retourne le driver trouvé. Gestion dans le service
        }

        public async Task<bool> UpdateAsync(Mission mission)
        {
            _context.Missions.Update(mission);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
