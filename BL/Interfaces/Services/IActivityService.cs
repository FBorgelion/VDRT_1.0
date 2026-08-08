using BL.DTOs.Activity;
using BL.DTOs.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IActivityService
    {
        public Task<IEnumerable<ActivityDto>> GetAllAsync();

        public Task<ActivityDto?> GetByIdAsync(int id);

        public Task<ActivityDto> AddAsync(CreateActivityDto createActivityDto);

        public Task<bool> UpdateAsync(int id, UpdateActivityDto updateActivityDto);

        Task<bool> DeleteAsync(int id);
    }
}
