using InstrumentsTracking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Domain.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<Exceptions> CreateAsync(Equipment entity);
        Task<Exceptions> DeleteAsync(int id);
        Task<IEnumerable<Equipment>> GetNonLinkedEquipment();
        Task<Exceptions> UpdateAsync(Equipment request, int id);
        Task<IEnumerable<Equipment>> GetAllAsync();
        Task<Equipment> GetAsync(int id);
    }
}
