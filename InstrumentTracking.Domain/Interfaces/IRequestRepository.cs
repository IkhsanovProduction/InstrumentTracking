using InstrumentsTracking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace InstrumentTracking.Domain.Interfaces
{
    public interface IRequestRepository
    {
        Task<Exceptions> CreateAsync(Request entity);
        Task DeleteAsync(int id);
        Task<Exceptions> UpdateAsync(Request request, int id);
        Task<IEnumerable<object>> GetAllAsync();
        Task<IEnumerable<object>> GetAllLinkedAsync();
        Task<IEnumerable<object>> GetLinkedAsync(int id);
        Task<IEnumerable<Request>> GetWhereAsync(Expression<Func<Request, bool>> predicate);
        Task<Request> GetAsync(int id);

    }
}
