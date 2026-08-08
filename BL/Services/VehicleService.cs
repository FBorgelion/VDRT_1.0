using AutoMapper;
using BL.DTOs.Driver;
using BL.DTOs.Vehicle;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class VehicleService: IVehicleService
    {

        protected readonly IMapper _mapper;
        protected readonly IVehicleRepo _vehicleRepo;

        public VehicleService(IMapper mapper, IVehicleRepo vehicleRepo)
        {
            _mapper = mapper;
            _vehicleRepo = vehicleRepo;
        }

        public async Task<VehicleDto> AddAsync(CreateVehicleDto createVehicleDto)
        {
            var vehicle = _mapper.Map<Vehicle>(createVehicleDto);

            var createdVehicle = await _vehicleRepo.AddAsync(vehicle);

            return _mapper.Map<VehicleDto>(createdVehicle);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(id);
            if (vehicle == null)
                return false;
            return await _vehicleRepo.DeleteAsync(vehicle);
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            var vehicles = await _vehicleRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto?> GetByIdAsync(int id)
        {
            var vehicle =  await _vehicleRepo.GetByIdAsync(id);
            if (vehicle == null)
                return default;
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<bool> UpdateAsync(int id, UpdateVehicleDto updateVehicleDto)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(id);

            if (vehicle == null)
                return false;

            _mapper.Map(updateVehicleDto, vehicle);
            vehicle.Id = id;

            return await _vehicleRepo.UpdateAsync(vehicle);
        }
    }
}
