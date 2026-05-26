using AutoMapper;
using BL.DTOs.Activity;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class ActivityService : IActivityService
    {

        protected readonly IMapper _mapper;
        protected readonly IActivityRepo _activityRepo;

        public ActivityService(IMapper mapper, IActivityRepo activityRepo)
        {
            _mapper = mapper;
            _activityRepo = activityRepo;

        }

        public async Task<ActivityDto> AddAsync(CreateActivityDto createActivityDto)
        {
            var activity = _mapper.Map<Activity>(createActivityDto);

            var createdActivity = await _activityRepo.AddAsync(activity);

            return _mapper.Map<ActivityDto>(createdActivity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var activity = await _activityRepo.GetByIdAsync(id);
            if (activity == null)
                return false;

            return await _activityRepo.DeleteAsync(activity);
        }

        public async Task<IEnumerable<ActivityDto>> GetAllAsync()
        {
            var activities = await _activityRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<ActivityDto?> GetByIdAsync(int id)
        {
            var activity = await _activityRepo.GetByIdAsync(id);

            if (activity == null)
                return default;

            return _mapper.Map<ActivityDto>(activity);

        }

        public async Task<bool> UpdateAsync(int id, UpdateActivityDto updateActivityDto)
        {
            var activity = await _activityRepo.GetByIdAsync(id);

            if (activity == null)
                return false;

            _mapper.Map(updateActivityDto, activity);
            activity.Id = id;

            return await _activityRepo.UpdateAsync(activity);
        }

    }
}
