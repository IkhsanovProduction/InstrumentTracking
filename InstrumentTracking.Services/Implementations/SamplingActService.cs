using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using InstrumentTracking.Domain.Interfaces;
using InstrumentTracking.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Services.Implementations
{
    public class SamplingActService : ISamplingActService
    {
        private readonly ISamplingActsRepository _samplingActRepository;

        public SamplingActService(ISamplingActsRepository samplingActRepository)
        {
            _samplingActRepository = samplingActRepository;
        }

        public async Task<Exceptions> CreateAsync(SamplingAct sampling)
        {
            var result = await _samplingActRepository.CreateAsync(sampling);
            return result;
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var result = await _samplingActRepository.DeleteAsync(id);
            return result;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            return await _samplingActRepository.GetAllAsync();
        }

        public async Task<SamplingAct> GetAsync(int id)
        {
            return await _samplingActRepository.GetAsync(id);
        }

        public async Task<Exceptions> UpdateAsync(SamplingAct request, int id)
        {
            var result = await _samplingActRepository.UpdateAsync(request, id);
            return result;
        }
    }
}
