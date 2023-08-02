using InstrumentTracking.Domain.Dtos;
using InstrumentTracking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;

namespace InstrumentTracking.Services.Implementations
{
    public class EngineerService : IEngineerService
    {
        private readonly IEngineerRepository _engineerRepository;

        public EngineerService(IEngineerRepository engineerRepository)
        {
            _engineerRepository = engineerRepository;
        }

        public async Task<Exceptions> CreateAsync(Engineer engineer)
        {
            var result = await _engineerRepository.CreateAsync(engineer);
            return result;
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var result = await _engineerRepository.DeleteAsync(id);
            return result;
        }

        public async Task<IEnumerable<Engineer>> GetAllAsync()
        {
            return await _engineerRepository.GetAllAsync();
        }

        public async Task<Engineer> GetAsync(int id)
        {
            return await _engineerRepository.GetAsync(id);
        }

        public async Task<Exceptions> UpdateAsync(Engineer request, int id)
        {
            var result = await _engineerRepository.UpdateAsync(request, id);
            return result;
        }
    }
}
