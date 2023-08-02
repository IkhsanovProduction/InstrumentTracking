using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InstrumentTracking.Domain.Dtos;
using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;

namespace InstrumentTracking.Services
{
    public interface IEngineerService
    {
        Task<Engineer> GetAsync(int id);
        Task<IEnumerable<Engineer>> GetAllAsync();
        Task<Exceptions> UpdateAsync(Engineer request, int id);
        Task<Exceptions> CreateAsync(Engineer engineer);
        Task<Exceptions> DeleteAsync(int id);
    }
}
