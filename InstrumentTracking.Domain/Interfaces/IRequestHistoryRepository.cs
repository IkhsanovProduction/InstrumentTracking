using InstrumentsTracking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Domain.Interfaces
{
    public interface IRequestHistoryRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<IEnumerable<object>> GetAllNonLinkedAsync();
        Task<IEnumerable<object>> GetLinkedAsync(int id);
        Task<Exceptions> DeleteAsync(int id);
    }
}
