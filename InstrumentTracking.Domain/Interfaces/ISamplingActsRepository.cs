using InstrumentsTracking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Domain.Interfaces
{
    public interface ISamplingActsRepository
    {
        Task<Exceptions> CreateAsync(SamplingAct entity);
        Task<Exceptions> DeleteAsync(int id);
        Task<Exceptions> UpdateAsync(SamplingAct request, int id);
        Task<IEnumerable<object>> GetAllAsync();
        Task<SamplingAct> GetAsync(int id);
    }
}
