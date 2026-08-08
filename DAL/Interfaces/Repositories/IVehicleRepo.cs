using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IVehicleRepo
    {

        public Task<IEnumerable<Vehicle>> GetAllAsync();
        public Task<Vehicle> GetByIdAsync(int id);
        public Task<Vehicle> AddAsync(Vehicle vehicle);
        public Task<bool> UpdateAsync(Vehicle vehicle);
        public Task<bool> DeleteAsync(Vehicle vehicle);

    }
}
