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
    public class EngineerRepository : IEngineerRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public EngineerRepository(AppDbContext context, ILogger<EngineerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Exceptions> CreateAsync(Engineer entity)
        {
            _context.Engineers.Add(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Create engineer {DT}", DateTime.UtcNow.ToLongTimeString());
            return Exceptions.Ok;
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var engineer = await _context.Engineers.FindAsync(id);

            if (engineer == null)
            {
                _logger.LogError("Cant delete engineer {DT}", DateTime.UtcNow.ToLongTimeString());
                return Exceptions.NotFound;
            }

            _context.Engineers.Remove(engineer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Delete engineer {DT}", DateTime.UtcNow.ToLongTimeString());
            return Exceptions.Ok;
        }

        public async Task<IEnumerable<Engineer>> GetAllAsync()
        {
            _logger.LogInformation("Get engineers {DT}", DateTime.UtcNow.ToLongTimeString());
            return await _context.Engineers.ToListAsync();
        }

        public async Task<Engineer> GetAsync(int id)
        {
            var engineer = await _context.Engineers.FindAsync(id);

            if (engineer != null)
            {
                _logger.LogInformation("Get engineer by id {DT}", DateTime.UtcNow.ToLongTimeString());
                return engineer;
            }

            return null;
        }

        public async Task<Exceptions> UpdateAsync(Engineer engineer, int id)
        {
            if (id != engineer.ID)
            {
                _logger.LogError("Cant put engineer {DT}", DateTime.UtcNow.ToLongTimeString());

                return Exceptions.BadRequest;
            }

            _context.Entry(engineer).State = EntityState.Modified;

            _logger.LogInformation("Update engineer {DT}", DateTime.UtcNow.ToLongTimeString());
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!EngineerExists(id))
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

        private bool EngineerExists(int id)
        {
            return _context.Engineers.Any(e => e.ID == id);
        }
    }
}
