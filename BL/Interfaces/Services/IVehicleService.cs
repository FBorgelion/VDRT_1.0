using BL.DTOs.Driver;
using BL.DTOs.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IVehicleService
    {

        public Task<IEnumerable<VehicleDto>> GetAllAsync();

        public Task<VehicleDto?> GetByIdAsync(int id);

        public Task<VehicleDto> AddAsync(CreateVehicleDto createVehicleDto);

        public Task<bool> UpdateAsync(int id, UpdateVehicleDto updateVehicleDto);

        Task<bool> DeleteAsync(int id);

    }
}
