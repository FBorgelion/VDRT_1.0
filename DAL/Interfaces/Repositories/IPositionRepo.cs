using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IPositionRepo
    {
        public Task<IEnumerable<Position>> GetAllAsync();
        public Task<Position> GetByIdAsync(int id);
        public Task<Position> AddAsync(Position position);
        public Task<bool> UpdateAsync(Position position);
        public Task<bool> DeleteAsync(Position position);
    }
}
