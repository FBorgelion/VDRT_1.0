using AutoMapper;
using BL.DTOs.Position;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class PositionService : IPositionService
    {

        protected readonly IMapper _mapper;
        protected readonly IPositionRepo _positionRepo;

        public PositionService(IMapper mapper, IPositionRepo positionRepo)
        {
            _mapper = mapper;
            _positionRepo = positionRepo;
        }

        public async Task<PositionDto> AddAsync(CreatePositionDto createPositionDto)
        {
            var position = _mapper.Map<Position>(createPositionDto);

            var createdPos = await _positionRepo.AddAsync(position);

            return _mapper.Map<PositionDto>(createdPos);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pos = await _positionRepo.GetByIdAsync(id);
            if (pos == null)
                return false;

            return await _positionRepo.DeleteAsync(pos);
        }

        public async Task<IEnumerable<PositionDto>> GetAllAsync()
        {
            var positions = await _positionRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<PositionDto>>(positions);
        }

        public async Task<PositionDto?> GetByIdAsync(int id)
        {
            var pos = await _positionRepo.GetByIdAsync(id);

            if (pos == null)
                return default;

            return _mapper.Map<PositionDto>(pos);

        }

        public async Task<bool> UpdateAsync(int id, UpdatePositionDto updatePositionDto)
        {
            var pos = await _positionRepo.GetByIdAsync(id);

            if (pos == null)
                return false;

            _mapper.Map(updatePositionDto, pos);
            pos.Id = id;

            return await _positionRepo.UpdateAsync(pos);
        }

    }
}
