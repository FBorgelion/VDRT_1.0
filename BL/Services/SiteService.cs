using AutoMapper;
using BL.DTOs.Driver;
using BL.DTOs.Site;
using BL.Interfaces.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class SiteService: ISiteService
    {
        protected readonly IMapper _mapper;
        protected readonly ISiteRepo _siteRepo;

        public SiteService(IMapper mapper, ISiteRepo siteRepo)
        {
            _mapper = mapper;
            _siteRepo = siteRepo;
        }

        public async Task<SiteDto> AddAsync(CreateSiteDto createSiteDto)
        {
            var site = _mapper.Map<Sites>(createSiteDto);

            var createdSite = await _siteRepo.AddAsync(site);

            return _mapper.Map<SiteDto>(createdSite);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var site = await _siteRepo.GetByIdAsync(id);
            if (site == null)
                return false;

            return await _siteRepo.DeleteAsync(site);
        }

        public async Task<IEnumerable<SiteDto>> GetAllAsync()
        {
            var sites = await _siteRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<SiteDto>>(sites);
        }

        public async Task<SiteDto?> GetByIdAsync(int id)
        {
            var site = await _siteRepo.GetByIdAsync(id);

            if (site == null)
                return default;

            return _mapper.Map<SiteDto>(site);

        }

        public async Task<bool> UpdateAsync(int id, UpdateSiteDto updateSiteDto)
        {
            var site = await _siteRepo.GetByIdAsync(id);

            if (site == null)
                return false;

            _mapper.Map(updateSiteDto, site);
            site.Id = id;

            return await _siteRepo.UpdateAsync(site);
        }
    }
}
