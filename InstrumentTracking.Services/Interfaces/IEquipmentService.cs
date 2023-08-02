using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Services.Interfaces
{
    public interface IEquipmentService
    {
        Task<Equipment> GetAsync(int id);
        Task<IEnumerable<Equipment>> GetAllAsync();
        Task<IEnumerable<Equipment>> GetNonLinkedEquipment();
        Task<Exceptions> UpdateAsync(Equipment equipment, int id);
        Task<Exceptions> CreateAsync(Equipment equipment);
        Task<Exceptions> DeleteAsync(int id);
    }
}
