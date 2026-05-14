using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IMissionRepo
    {

        public Task<IEnumerable<Mission>> GetAllAsync();
        public Task<Mission> GetByIdAsync(int id);
        public Task<Mission> AddAsync(Mission mission);
        public Task<bool> UpdateAsync(Mission mission);
        public Task<bool> DeleteAsync(Mission mission);

    }
}
