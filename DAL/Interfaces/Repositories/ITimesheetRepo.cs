using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface ITimesheetRepo
    {
        public Task<IEnumerable<Timesheet>> GetAllAsync();
        public Task<Timesheet> GetByIdAsync(int id);
        public Task<Timesheet> AddAsync(Timesheet timesheet);
        public Task<bool> UpdateAsync(Timesheet timesheet);
        public Task<bool> DeleteAsync(Timesheet timesheet);
    }
}
