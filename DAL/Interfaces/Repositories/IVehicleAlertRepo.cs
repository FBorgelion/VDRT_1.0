using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IVehicleAlertRepo
    {
        public Task<IEnumerable<VehicleAlert>> GetAllAsync();
        public Task<VehicleAlert> GetByIdAsync(int id);
        public Task<VehicleAlert> AddAsync(VehicleAlert alert);
        public Task<bool> UpdateAsync(VehicleAlert alert);
        public Task<bool> DeleteAsync(VehicleAlert alert);
    }
}
