using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface ISiteRepo
    {

        public Task<IEnumerable<Sites>> GetAllAsync();
        public Task<Sites> GetByIdAsync(int id);
        public Task<Sites> AddAsync(Sites sire);
        public Task<bool> UpdateAsync(Sites site);
        public Task<bool> DeleteAsync(Sites site);

    }
}
