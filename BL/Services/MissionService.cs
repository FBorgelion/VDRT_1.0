using AutoMapper;
using BL.DTOs.Mission;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class MissionService : IMissionService
    {

        protected readonly IMapper _mapper;
        protected readonly IMissionRepo _missionRepo;

        public MissionService(IMapper mapper, IMissionRepo missionRepo)
        {
            _mapper = mapper;
            _missionRepo = missionRepo;

        }

        public async Task<MissionDto> AddAsync(CreateMissionDto createMissionDto)
        {
            var mission = _mapper.Map<Mission>(createMissionDto);

            var createdMission = await _missionRepo.AddAsync(mission);

            return _mapper.Map<MissionDto>(createdMission);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mission = await _missionRepo.GetByIdAsync(id);
            if (mission == null)
                return false;

            return await _missionRepo.DeleteAsync(mission);
        }

        public async Task<IEnumerable<MissionDto>> GetAllAsync()
        {
            var missions = await _missionRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<MissionDto>>(missions);
        }

        public async Task<MissionDto?> GetByIdAsync(int id)
        {
            var mission = await _missionRepo.GetByIdAsync(id);

            if (mission == null)
                return default;

            return _mapper.Map<MissionDto>(mission);

        }

        public async Task<bool> UpdateAsync(int id, UpdateMissionDto updateMissionDto)
        {
            var mission = await _missionRepo.GetByIdAsync(id);

            if (mission == null)
                return false;

            _mapper.Map(updateMissionDto, mission);
            mission.Id = id;

            return await _missionRepo.UpdateAsync(mission);
        }

    }
}
