using BL.DTOs.Driver;
using BL.DTOs.TripAnomaly;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface ITripAnomalyService
    {

        public Task<IEnumerable<TripAnomalyDto>> GetAllAsync();

        public Task<TripAnomalyDto?> GetByIdAsync(int id);

        public Task<TripAnomalyDto> AddAsync(CreateTripAnomalyDto createTripAnomalyDto);

        public Task<bool> UpdateAsync(int id, UpdateTripAnomalyDto updateTripAnomalyDto);

        Task<bool> DeleteAsync(int id);


    }
}
