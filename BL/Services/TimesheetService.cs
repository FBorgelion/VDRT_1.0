using AutoMapper;
using BL.DTOs.Driver;
using BL.DTOs.Timesheet;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class TimesheetService : ITimesheetService
    {

        protected readonly IMapper _mapper;
        protected readonly ITimesheetRepo _timesheetRepo;

        public TimesheetService(IMapper mapper, ITimesheetRepo timesheetRepo)
        {
            _mapper = mapper;
            _timesheetRepo = timesheetRepo;
        }

        public async Task<TimesheetDto> AddAsync(CreateTimesheetDto createTimesheetDto)
        {
            var timesheet = _mapper.Map<Timesheet>(createTimesheetDto);

            var createdTimesheet = await _timesheetRepo.AddAsync(timesheet);

            return _mapper.Map<TimesheetDto>(createdTimesheet);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var timesheet = await _timesheetRepo.GetByIdAsync(id);
            if (timesheet == null)
                return false;

            return await _timesheetRepo.DeleteAsync(timesheet);
        }

        public async Task<IEnumerable<TimesheetDto>> GetAllAsync()
        {
            var timesheets = await _timesheetRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<TimesheetDto>>(timesheets);        
        }

        public async Task<TimesheetDto?> GetByIdAsync(int id)
        {
            var timesheet = await _timesheetRepo.GetByIdAsync(id);

            if (timesheet == null)
                return default;

            return _mapper.Map<TimesheetDto>(timesheet);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTimesheetDto updateTimesheetDto)
        {
            var timesheet = await _timesheetRepo.GetByIdAsync(id);

            if (timesheet == null)
                return false;

            _mapper.Map(updateTimesheetDto, timesheet);
            timesheet.Id = id;

            return await _timesheetRepo.UpdateAsync(timesheet);
        }
    }
}
