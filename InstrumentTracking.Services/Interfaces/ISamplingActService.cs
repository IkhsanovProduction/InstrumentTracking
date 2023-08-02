using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Services.Interfaces
{
    public interface ISamplingActService
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<SamplingAct> GetAsync(int id);
        Task<Exceptions> UpdateAsync(SamplingAct request, int id);
        Task<Exceptions> CreateAsync(SamplingAct samplingAct);
        Task<Exceptions> DeleteAsync(int id);
    }
}
