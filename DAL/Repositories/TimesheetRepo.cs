using DAL.Data;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class TimesheetRepo : ITimesheetRepo
    {

        private readonly AppDbContext _context;

        public TimesheetRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Timesheet> AddAsync(Timesheet timesheet)
        {
            _context.Timesheets.Add(timesheet);
            await _context.SaveChangesAsync();
            return timesheet;
        }

        public async Task<bool> DeleteAsync(Timesheet timesheet)
        {
            _context.Timesheets.Remove(timesheet);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Timesheet>> GetAllAsync()
        {
            return await _context.Timesheets.ToListAsync();
        }

        public async Task<Timesheet> GetByIdAsync(int id)
        {
            return await _context.Timesheets.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Timesheet timesheet)
        {
            _context.Timesheets.Update(timesheet);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
