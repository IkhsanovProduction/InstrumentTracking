using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using InstrumentTracking.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Services
{
    public interface IRequestHistoryService
    {
        Task<Exceptions> DeleteAsync(int id);
        Task<IEnumerable<object>> GetAllAsync();
        Task<IEnumerable<object>> GetAllLinkedAsync();
        Task<IEnumerable<object>> GetLinkedAsync(int id);
        Task<IEnumerable<Request>> GetWhereAsync(Expression<Func<Request, bool>> predicate);
    }
}
