using BL.DTOs.Driver;
using BL.DTOs.VehicleAlert;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IVehicleAlertService
    {
        public Task<IEnumerable<VehicleAlertDto>> GetAllAsync();

        public Task<VehicleAlertDto?> GetByIdAsync(int id);

        public Task<VehicleAlertDto> AddAsync(CreateVehicleAlertDto createVehicleAlertDto);

        public Task<bool> UpdateAsync(int id, UpdateVehicleAlertDto updateVehicleAlertDto);

        Task<bool> DeleteAsync(int id);
    }
}
