using BL.DTOs.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface ISiteService
    {
        public Task<IEnumerable<SiteDto>> GetAllAsync();

        public Task<SiteDto?> GetByIdAsync(int id);

        public Task<SiteDto> AddAsync(CreateSiteDto createSiteDto);

        public Task<bool> UpdateAsync(int id, UpdateSiteDto updateSiteDto);

        Task<bool> DeleteAsync(int id);
    }
}
