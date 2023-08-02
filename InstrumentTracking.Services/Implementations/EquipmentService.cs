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
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public async Task<Exceptions> CreateAsync(Equipment equipment)
        {
            var result = await _equipmentRepository.CreateAsync(equipment);
            return result;
        }

        public async Task<IEnumerable<Equipment>> GetNonLinkedEquipment()
        {
            return await _equipmentRepository.GetNonLinkedEquipment();
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var result = await _equipmentRepository.DeleteAsync(id);
            return result;
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            return await _equipmentRepository.GetAllAsync();
        }

        public async Task<Equipment> GetAsync(int id)
        {
            return await _equipmentRepository.GetAsync(id);
        }

        public async Task<Exceptions> UpdateAsync(Equipment request, int id)
        {
            var result = await _equipmentRepository.UpdateAsync(request, id);
            return result;
        }
    }
}
