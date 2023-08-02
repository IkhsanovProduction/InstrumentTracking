using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InstrumentTracking.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InstrumentTracking.Domain;
using InstrumentsTracking.Services;

namespace InstrumentTracking.Infrastructure.Repositories
{
    public class RequestsHistoryRepository : IRequestHistoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public RequestsHistoryRepository(AppDbContext context, ILogger<RequestsHistoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var reqHistory = await _context.RequestHistory.ToListAsync();
            
            foreach(var rh in reqHistory)
            {
                _context.RequestHistory.Remove(rh);
            }
           
            await _context.SaveChangesAsync();

            _logger.LogInformation("Delete engineer {DT}", DateTime.UtcNow.ToLongTimeString());
            return Exceptions.Ok;
        }


        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var request = from req in _context.RequestHistory
                          join engineer in _context.Engineers on req.EngineerID equals engineer.ID
                          join equipment in _context.Equipment on req.EquipmentID equals equipment.ID

                          select new
                          {
                              ID = req.ID,
                              EngineerID = req.EngineerID,
                              EquipmentID = req.EquipmentID,
                              Equipment = equipment.Name,
                              EngineerName = engineer.Name,
                              EngineerSurname = engineer.Surname,

                              StatusID = req.StatusID,
                              Date = req.Date,
                              WellNumber = req.WellNumber,
                              Comment = req.Comment
                          };

            _logger.LogInformation("Get all requests {DT}", DateTime.UtcNow.ToLongTimeString());
            return await request.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllNonLinkedAsync()
        {
            var request = from req in _context.RequestHistory
                          join engineer in _context.Engineers on req.EngineerID equals engineer.ID
                          join equipment in _context.Equipment on req.EquipmentID equals equipment.ID

                          select new
                          {
                              ID = req.ID,
                              EquipmentID = req.EquipmentID,
                              EngineerName = engineer.Name,
                              EngineerSurname = engineer.Surname,
                              Date = req.Date,
                              StatusID = req.StatusID,
                              WellNumber = req.WellNumber,
                          };

            return await request.Where(req => req.StatusID == (int)Status.NonLinked).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetLinkedAsync(int id)
        {
            var request = from req in _context.RequestHistory
                          join engineer in _context.Engineers on req.EngineerID equals engineer.ID
                          join equipment in _context.Equipment on req.EquipmentID equals equipment.ID

                          select new
                          {
                              ID = req.EngineerID,
                              RequestID = req.ID,
                              EquipmentID = req.EquipmentID,
                              Equipment = equipment.Name,
                              SerialNumber = equipment.SerialNumber,
                              InventoryNumber = equipment.InventoryNumber,
                              DateOfCalibrationTest = equipment.DateOfCalibrationTest,
                              DateOfTheNextCalibrationTest = equipment.DateOfTheNextCalibrationTest,
                              ResponsibleForCalibration = equipment.ResponsibleForCalibration,
                              Passport = equipment.Passport,

                              EngineerSurname = engineer.Surname,
                              EngineerName = engineer.Name,
                              Phone = engineer.Phone,

                              StatusID = req.StatusID,
                              Date = req.Date,
                              WellNumber = req.WellNumber,
                              Comment = req.Comment
                          };

            return await request.Where(req => req.ID == id && req.StatusID == (int)Status.Linked).ToListAsync();
        }
    }
}
