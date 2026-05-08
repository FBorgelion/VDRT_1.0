using AutoMapper;
using BL.DTOs.Driver;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class DriverService : IDriverService
    {

        protected readonly IMapper _mapper;
        protected readonly IDriverRepo _driverRepo;

        public DriverService(IMapper mapper, IDriverRepo driverRepo)
        {
            _mapper = mapper;
            _driverRepo = driverRepo;

        }

        public async Task<DriverDto> AddAsync(CreateDriverDto createDriverDto)
        {
            var driver = _mapper.Map<Driver>(createDriverDto);

            var createdDriver = await _driverRepo.AddAsync(driver);

            return _mapper.Map<DriverDto>(createdDriver);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var driver = await _driverRepo.GetByIdAsync(id);
            if (driver == null)
                return false;

            return await _driverRepo.DeleteAsync(driver);
        }

        public async Task<IEnumerable<DriverDto>> GetAllAsync()
        {
            var drivers = await _driverRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }

        public async Task<DriverDto?> GetByIdAsync(int id)
        {
            var driver = await _driverRepo.GetByIdAsync(id);

            if (driver == null)
                return default;

            return _mapper.Map<DriverDto>(driver);

        }

        public async Task<bool> UpdateAsync(int id, UpdateDriverDto updateDriverDto)
        {
            var driver = await _driverRepo.GetByIdAsync(id);

            if (driver == null)
                return false;

            _mapper.Map(updateDriverDto, driver);
            driver.Id = id;

            return await _driverRepo.UpdateAsync(driver);
        }
    }
}