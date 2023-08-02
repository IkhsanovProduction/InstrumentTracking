using InstrumentsTracking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Domain.Interfaces
{
    public interface IEngineerRepository
    {
        Task<Exceptions> CreateAsync(Engineer entity);
        Task<Exceptions> DeleteAsync(int id);
        Task<Exceptions> UpdateAsync(Engineer request, int id);
        Task<IEnumerable<Engineer>> GetAllAsync();
        Task<Engineer> GetAsync(int id);
    }
}
