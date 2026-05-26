using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IActivityRepo
    {
        public Task<IEnumerable<Activity>> GetAllAsync();
        public Task<Activity> GetByIdAsync(int id);
        public Task<Activity> AddAsync(Activity activity);
        public Task<bool> UpdateAsync(Activity activity);
        public Task<bool> DeleteAsync(Activity activity);
    }
}
