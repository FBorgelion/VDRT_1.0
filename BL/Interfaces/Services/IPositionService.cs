using BL.DTOs.Driver;
using BL.DTOs.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IPositionService
    {
        public Task<IEnumerable<PositionDto>> GetAllAsync();

        public Task<PositionDto?> GetByIdAsync(int id);

        public Task<PositionDto> AddAsync(CreatePositionDto createPositionDto);

        public Task<bool> UpdateAsync(int id, UpdatePositionDto updatePositionDto);

        Task<bool> DeleteAsync(int id);
    }
}
