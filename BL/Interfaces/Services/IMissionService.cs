using BL.DTOs.Mission;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IMissionService
    {

        public Task<IEnumerable<MissionDto>> GetAllAsync();

        public Task<MissionDto?> GetByIdAsync(int id);

        public Task<MissionDto> AddAsync(CreateMissionDto createMissionDto);

        public Task<bool> UpdateAsync(int id, UpdateMissionDto updateMissionDto);

        Task<bool> DeleteAsync(int id);

    }
}
