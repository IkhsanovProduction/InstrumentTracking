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
    public enum Status
    {
        Linked = 1,
        NonLinked = 2
    }

    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;

        public RequestRepository(AppDbContext context, ILogger<RequestRepository> logger)
        {
            _context = context;
            _logger = logger;
            _notificationService = new TelegramNotification();
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var request = from req in _context.Request
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
                              BindingDate = req.BindingDate,
                              UnbindingDate = req.UnbindingDate,
                              WellNumber = req.WellNumber,
                              Comment = req.Comment,

                              GPSLatitude = req.GPSLatitude,
                              GPSLongitude = req.GPSLongitude,
                              GPSExactLocation = req.GPSExactLocation
                          };

            _logger.LogInformation("Get all requests {DT}", DateTime.UtcNow.ToLongTimeString());
            return await request.ToListAsync();
        }

        public async Task<Request> GetAsync(int id)
        {
            var entity = await _context.Request.FindAsync(id);
            _logger.LogInformation("Get request {DT} {entity}", DateTime.UtcNow.ToLongTimeString(), entity);
            
            return entity;
        }

        private RequestHistory AddRequestHistory(Request request)
        {
            RequestHistory requestHistory = new RequestHistory()
            {
                Date = request.UnbindingDate,
                Comment = request.Comment,
                StatusID = request.StatusID,
                WellNumber = request.WellNumber,

                EngineerID = request.EngineerID,
                EquipmentID = request.EquipmentID
            };

            switch (request.StatusID)
            {
                case (int)Status.Linked:
                    requestHistory.Date = request.BindingDate;
                    break;
                case (int)Status.NonLinked:
                    requestHistory.Date = request.UnbindingDate;
                    break;
            }

            return requestHistory;
        }

        public async Task<Exceptions> CreateAsync(Request request)
        {
            if (_context.Request.Where(linkedRequest => linkedRequest.EquipmentID == request.EquipmentID
                                             && linkedRequest.StatusID == (int)Status.Linked).Any())
            {
                _logger.LogError("Request is exist {DT}", DateTime.UtcNow.ToLongTimeString());
                return Exceptions.Request_exist;
            }
            
            else
            {    
                Equipment equipment = _context.Equipment.FindAsync(request.EquipmentID).Result;
                Engineer engineer = _context.Engineers.FindAsync(request.EngineerID).Result;

                if (equipment != null)
                {
                    equipment.IsLinked = true;

                    _context.Entry(equipment).State = EntityState.Modified;

                    _context.Request.Add(request);

                    _context.RequestHistory.Add(AddRequestHistory(request));

                    await _context.SaveChangesAsync();

                    _notificationService.SendNotificationBind(equipment.Name, engineer.Surname);

                    _logger.LogInformation("Create new request {DT}", DateTime.UtcNow.ToLongTimeString());

 
                    return Exceptions.Ok;
                }

                return Exceptions.NotFound;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request != null)
            {
                Equipment equipment = _context.Equipment.FindAsync(request.EquipmentID).Result;

                equipment.IsLinked = false;

                _context.Entry(equipment).State = EntityState.Modified;

                var entity = await GetAsync(id);
                await DeleteAsync(entity);
                _logger.LogInformation("Delete request {DT}", DateTime.UtcNow.ToLongTimeString());
            }
        }

        public async Task DeleteAsync(Request entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Request.Attach(entity);

            _context.Request.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Request>> GetWhereAsync(Expression<Func<Request, bool>> predicate)
        {
            return await _context.Request.Where(predicate).ToListAsync();
        }

        public async Task<Exceptions> UpdateAsync(Request request, int id)
        {
            if (request == null)
            {
                return Exceptions.NotFound;
            }


            if (request.ID == id)
            {
                Equipment equipment = _context.Equipment.FindAsync(request.EquipmentID).Result;
                Engineer engineer = _context.Engineers.FindAsync(request.EngineerID).Result;
                Request oldRequest = _context.Request.AsNoTracking().Where(x => x.ID == id).FirstOrDefault();

                if (equipment == null)
                {
                    return Exceptions.NotFound;
                }

                if (oldRequest.WellNumber != request.WellNumber)
                {
                    _notificationService.SentNotificationChangeWellNumber(engineer.Surname, equipment.Name, oldRequest.WellNumber, request.WellNumber);
                }

                if (request.StatusID == (int)Status.NonLinked)
                {
                    equipment.IsLinked = false;

                    _notificationService.SentNotificationUnbind(equipment.Name, engineer.Surname);
                }

                else { equipment.IsLinked = true; }

                _context.RequestHistory.Add(AddRequestHistory(request));

                _context.Entry(equipment).State = EntityState.Modified;
                _context.Entry(request).State = EntityState.Modified;


                await _context.SaveChangesAsync();

                _logger.LogInformation("Update request {DT}", DateTime.UtcNow.ToLongTimeString());
                return Exceptions.Ok;
            }

            return Exceptions.BadRequest;
        }

        public async Task<IEnumerable<object>> GetAllLinkedAsync()
        {
            var request = from req in _context.Request
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
                              BindingDate = req.BindingDate,
                              UnbindingDate = req.UnbindingDate,
                              WellNumber = req.WellNumber,
                              Comment = req.Comment,

                              GPSLatitude = req.GPSLatitude,
                              GPSLongitude = req.GPSLongitude,
                              GPSExactLocation = req.GPSExactLocation
                          };

            var linkedReq = await request.Where(req => req.StatusID == (int)Status.Linked).ToListAsync();
            return linkedReq.GroupBy(t => t.WellNumber).Select(b => b.FirstOrDefault()).ToList();
        }

        public async Task<IEnumerable<object>> GetLinkedAsync(int id)
        {
            var request = from req in _context.Request
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

                              Status = req.StatusID,
                              BindingDate = req.BindingDate,
                              UnbindingDate = req.UnbindingDate,
                              WellNumber = req.WellNumber,
                              Comment = req.Comment,

                              GPSLatitude = req.GPSLatitude,
                              GPSLongitude = req.GPSLongitude,

                              GPSExactLocation = req.GPSExactLocation
                          };

            return await request.Where(req => req.ID == id && req.Status == (int)Status.Linked).ToListAsync();
        }
    }
}
