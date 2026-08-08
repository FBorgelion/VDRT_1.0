using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface ITripAnomalyRepo
    {

        public Task<IEnumerable<TripAnomaly>> GetAllAsync();
        public Task<TripAnomaly> GetByIdAsync(int id);
        public Task<TripAnomaly> AddAsync(TripAnomaly tripAnomaly);
        public Task<bool> UpdateAsync(TripAnomaly tripAnomaly);
        public Task<bool> DeleteAsync(TripAnomaly tripAnomaly);

    }
}
