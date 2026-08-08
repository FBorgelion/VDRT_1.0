using BL.DTOs.Driver;
using BL.DTOs.Timesheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface ITimesheetService
    {
        public Task<IEnumerable<TimesheetDto>> GetAllAsync();

        public Task<TimesheetDto?> GetByIdAsync(int id);

        public Task<TimesheetDto> AddAsync(CreateTimesheetDto createTimesheetDto);

        public Task<bool> UpdateAsync(int id, UpdateTimesheetDto updateTimesheetDto);

        Task<bool> DeleteAsync(int id);
    }
}
