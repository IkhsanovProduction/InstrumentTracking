using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using InstrumentTracking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Infrastructure.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public EquipmentRepository(AppDbContext context, ILogger<EngineerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Equipment>> GetNonLinkedEquipment()
        {
            return await _context.Equipment.Where(x => x.IsLinked == false).ToListAsync();
        }

        public async Task<Exceptions> CreateAsync(Equipment entity)
        {
            _context.Equipment.Add(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Create equipment {DT}", DateTime.UtcNow.ToLongTimeString());
            return Exceptions.Ok;
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
            {
                _logger.LogError("Cant delete equipment {DT}", DateTime.UtcNow.ToLongTimeString());
                return Exceptions.NotFound;
            }

            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Delete equipment {DT}", DateTime.UtcNow.ToLongTimeString());
            return Exceptions.Ok;
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            _logger.LogInformation("Get equipments {DT}", DateTime.UtcNow.ToLongTimeString());
            return await _context.Equipment.ToListAsync();
        }

        public async Task<Equipment> GetAsync(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment != null)
            {
                _logger.LogInformation("Get equipment by id {DT}", DateTime.UtcNow.ToLongTimeString());
                return equipment;
            }

            return null;
        }

        public async Task<Exceptions> UpdateAsync(Equipment equipment, int id)
        {
            if (id != equipment.ID)
            {
                _logger.LogError("Cant put equipment {DT}", DateTime.UtcNow.ToLongTimeString());

                return Exceptions.BadRequest;
            }

            _context.Entry(equipment).State = EntityState.Modified;

            _logger.LogInformation("Update equipment {DT}", DateTime.UtcNow.ToLongTimeString());
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
                {
                    return Exceptions.NotFound;
                }
                else
                {
                    throw;
                }
            }

            return Exceptions.Ok;
        }

        private bool EquipmentExists(int id)
        {
            return _context.Engineers.Any(e => e.ID == id);
        }

    }
}
