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
    public class RequestsHistoryService : IRequestHistoryService
    {
        private readonly IRequestHistoryRepository _requestRepository;
        private ILogger _logger;

        public RequestsHistoryService(IRequestHistoryRepository requestRepository, ILogger<RequestsService> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;
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
            return await _requestRepository.GetAllNonLinkedAsync();
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var result = await _requestRepository.DeleteAsync(id);
            return result;
        }

        public async Task<IEnumerable<object>> GetLinkedAsync(int id)
        {
            return await _requestRepository.GetLinkedAsync(id);
        }

        public Task<IEnumerable<Request>> GetWhereAsync(Expression<Func<Request, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
