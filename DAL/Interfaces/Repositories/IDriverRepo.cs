using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IDriverRepo
    {

        public Task<IEnumerable<Driver>> GetAllAsync();
        public Task<Driver> GetByIdAsync(int id);
        public Task<Driver> AddAsync(Driver driver);
        public Task<bool> UpdateAsync(Driver driver);
        public Task<bool> DeleteAsync(Driver driver);

    }
}
