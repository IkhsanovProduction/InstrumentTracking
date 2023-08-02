using InstrumentsTracking.Domain.Models;
using InstrumentsTracking.Exception;
using InstrumentTracking.Domain;
using InstrumentTracking.Domain.Dtos;
using InstrumentTracking.Domain.Interfaces;
using InstrumentTracking.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Services.Implementations
{
    public class RequestsService : IRequestsService
    {
        private readonly IRequestRepository _requestRepository;
        private ILogger _logger;

        public RequestsService(IRequestRepository requestRepository, ILogger<RequestsService> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;
        }

        public async Task<Exceptions> CreateAsync(Request entity)
        {
            var result = await _requestRepository.CreateAsync(entity);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _requestRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();

            try
            {
                response.Data = true;

                return await _requestRepository.GetAllAsync();  
            }

            catch (Exception exception)
            {
                response.Errors.Add(exception.Message);
            }

            return null;
        }

        public async Task<IEnumerable<object>> GetAllLinkedAsync()
        {
            return  await _requestRepository.GetAllLinkedAsync();
        }

        public async Task<Request> GetAsync(int id)
        {
            return await _requestRepository.GetAsync(id);
        }

        public async Task<IEnumerable<object>> GetLinkedAsync(int id)
        {
            return await _requestRepository.GetLinkedAsync(id);
        }

        public Task<IEnumerable<Request>> GetWhereAsync(Expression<Func<Request, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Exceptions> UpdateAsync(Request entity, int id)
        {   
            var result = await _requestRepository.UpdateAsync(entity, id);
            return result;
        }
    }
}
