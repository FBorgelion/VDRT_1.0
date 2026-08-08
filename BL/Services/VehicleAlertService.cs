using AutoMapper;
using BL.DTOs.Driver;
using BL.DTOs.VehicleAlert;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class VehicleAlertService : IVehicleAlertService
    {

        protected readonly IMapper _mapper;
        protected readonly IVehicleAlertRepo _vehicleAlertRepo;

        public VehicleAlertService(IMapper mapper, IVehicleAlertRepo vehicleAlertRepo)
        {
            _mapper = mapper;
            _vehicleAlertRepo = vehicleAlertRepo;
        }

        public async Task<VehicleAlertDto> AddAsync(CreateVehicleAlertDto createVehicleAlertDto)
        {
            var alert = _mapper.Map<VehicleAlert>(createVehicleAlertDto);

            var createdVehicleAlert = await _vehicleAlertRepo.AddAsync(alert);

            return _mapper.Map<VehicleAlertDto>(createdVehicleAlert);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alert = await _vehicleAlertRepo.GetByIdAsync(id);
            if (alert == null)
                return false;

            return await _vehicleAlertRepo.DeleteAsync(alert);
        }

        public async Task<IEnumerable<VehicleAlertDto>> GetAllAsync()
        {
            var alerts = await _vehicleAlertRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleAlertDto>>(alerts);
        }

        public async Task<VehicleAlertDto?> GetByIdAsync(int id)
        {
            var alert = await _vehicleAlertRepo.GetByIdAsync(id);

            if (alert == null)
                return default;

            return _mapper.Map<VehicleAlertDto>(alert);
        }

        public async Task<bool> UpdateAsync(int id, UpdateVehicleAlertDto updateVehicleAlertDto)
        {
            var alert = await _vehicleAlertRepo.GetByIdAsync(id);

            if (alert == null)
                return false;

            _mapper.Map(updateVehicleAlertDto, alert);
            alert.Id = id;

            return await _vehicleAlertRepo.UpdateAsync(alert);
        }
    }
}
