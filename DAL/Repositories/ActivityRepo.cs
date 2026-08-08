using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ActivityRepo : IActivityRepo
    {

        private readonly AppDbContext _context;

        public ActivityRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Activity> AddAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<bool> DeleteAsync(Activity activity)
        {
            _context.Activities.Remove(activity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _context.Activities.ToListAsync();
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            return await _context.Activities.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
