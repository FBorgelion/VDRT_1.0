using BL.DTOs.Driver;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IDriverService
    {

        public Task<IEnumerable<DriverDto>> GetAllAsync();

        public Task<DriverDto?> GetByIdAsync(int id);

        public Task<DriverDto> AddAsync(CreateDriverDto createDriverDto);

        public Task<bool> UpdateAsync(int id, UpdateDriverDto updateDriverDto);

        Task<bool> DeleteAsync(int id);

    }
}
