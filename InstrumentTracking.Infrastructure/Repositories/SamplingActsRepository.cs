using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using InstrumentTracking.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using InstrumentsTracking.Services;

namespace InstrumentTracking.Infrastructure.Repositories
{
    public class SamplingActsRepository : ISamplingActsRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;

        public SamplingActsRepository(AppDbContext context,  
                                      ILogger<SamplingActsRepository> logger)
        {
            _context = context;
            _logger = logger;
            _notificationService = new TelegramNotification();
        }


        public async Task<Exceptions> CreateAsync(SamplingAct entity)
        {
            Engineer engineer = _context.Engineers.FindAsync(entity.EngineerID).Result;

            _context.SamplingAct.Add(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Create samplingAct {DT}", DateTime.UtcNow.ToLongTimeString());

            _notificationService.SendNotificationSamplingAct(entity.WellNumber, engineer.Surname);
            return Exceptions.Ok;
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var sampling = await _context.SamplingAct.FindAsync(id);

            if (sampling == null)
            {
                _logger.LogError("Cant delete samplingAct {DT}", DateTime.UtcNow.ToLongTimeString());
                return Exceptions.NotFound;
            }

            _context.SamplingAct.Remove(sampling);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Delete sampling {DT}", DateTime.UtcNow.ToLongTimeString());
            return Exceptions.Ok;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var samplingAct = from sample in _context.SamplingAct
                              join engineer in _context.Engineers on sample.EngineerID equals engineer.ID


                              select new
                              {
                                  id = sample.ID,
                                  engineerID = sample.EngineerID,
                                  engineerName = engineer.Name,
                                  engineerSurname = engineer.Surname,

                                  wellNumber = sample.WellNumber,
                                  placeOfBirth = sample.PlaceOfBirth,
                                  drillingDepth = sample.DrillingDepth,
                                  typeOfSolution = sample.TypeOfSolution,
                                  workingCapacity = sample.WorkingCapacity,
                                  selectionDate = sample.SelectionDate,
                                  sampleVolume = sample.SampleVolume
                              };

            return await samplingAct.ToListAsync();
        }

        public async Task<SamplingAct> GetAsync(int id)
        {
            var sampling = await _context.SamplingAct.FindAsync(id);

            if (sampling != null)
            {
                _logger.LogInformation("Get engineer by id {DT}", DateTime.UtcNow.ToLongTimeString());
                return sampling;
            }

            return null;
        }

        public async Task<Exceptions> UpdateAsync(SamplingAct request, int id)
        {
            if (id != request.ID)
            {
                _logger.LogError("Cant put samplingAct {DT}", DateTime.UtcNow.ToLongTimeString());

                return Exceptions.BadRequest;
            }

            _context.Entry(request).State = EntityState.Modified;

            _logger.LogInformation("Update samplingAct {DT}", DateTime.UtcNow.ToLongTimeString());
            
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!SamplingActExists(id))
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

        private bool SamplingActExists(int id)
        {
            return _context.SamplingAct.Any(e => e.ID == id);
        }
    }
}
