using AutoMapper;
using BL.DTOs.Driver;
using BL.DTOs.TripAnomaly;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class TripAnomalyService : ITripAnomalyService
    {

        protected readonly IMapper _mapper;
        protected readonly ITripAnomalyRepo _tripAnomalyRepo;

        public TripAnomalyService(IMapper mapper, ITripAnomalyRepo tripAnomalyRepo)
        {
            _mapper = mapper;
            _tripAnomalyRepo = tripAnomalyRepo;
        }

        public async Task<TripAnomalyDto> AddAsync(CreateTripAnomalyDto createTripAnomalyDto)
        {
            var anomaly = _mapper.Map<TripAnomaly>(createTripAnomalyDto);

            var createdAnomaly = await _tripAnomalyRepo.AddAsync(anomaly);

            return _mapper.Map<TripAnomalyDto>(createdAnomaly);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var anomaly = await _tripAnomalyRepo.GetByIdAsync(id);
            if (anomaly == null)
                return false;

            return await _tripAnomalyRepo.DeleteAsync(anomaly);
        }

        public async Task<IEnumerable<TripAnomalyDto>> GetAllAsync()
        {
            var anomalies = await _tripAnomalyRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<TripAnomalyDto>>(anomalies);
        }

        public async Task<TripAnomalyDto?> GetByIdAsync(int id)
        {
            var anomaly = await _tripAnomalyRepo.GetByIdAsync(id);

            if (anomaly == null)
                return default;

            return _mapper.Map<TripAnomalyDto>(anomaly);

        }

        public async Task<bool> UpdateAsync(int id, UpdateTripAnomalyDto updateTripAnomalyDto)
        {
            var anomaly = await _tripAnomalyRepo.GetByIdAsync(id);

            if (anomaly == null)
                return false;

            _mapper.Map(updateTripAnomalyDto, anomaly);
            anomaly.Id = id;

            return await _tripAnomalyRepo.UpdateAsync(anomaly);
        }

    }
}
