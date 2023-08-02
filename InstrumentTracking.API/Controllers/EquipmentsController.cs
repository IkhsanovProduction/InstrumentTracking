using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InstrumentsTracking;
using InstrumentsTracking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using InstrumentTracking.Services.Interfaces;
using InstrumentsTracking.Exception;

namespace InstrumentsTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentsController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("/NonLinkedEquipments")]
        public async Task<IEnumerable<object>> GetNonLinkedEquipments()
        {
            return await _equipmentService.GetNonLinkedEquipment();
        }

        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> Post(Equipment equipment)
        {
            var result = await _equipmentService.CreateAsync(equipment);

            return result switch
            {
                InstrumentTracking.Domain.Exceptions.BadRequest => BadRequest(),
                InstrumentTracking.Domain.Exceptions.Request_exist => new CustomException(System.Net.HttpStatusCode.Locked, "Прибор уже привязан"),
                InstrumentTracking.Domain.Exceptions.NotFound => NotFound(),
                InstrumentTracking.Domain.Exceptions.Ok => Ok(),
                _ => BadRequest(),
            };
        }

        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _equipmentService.DeleteAsync(id);

            return result switch
            {
                InstrumentTracking.Domain.Exceptions.BadRequest => BadRequest(),
                InstrumentTracking.Domain.Exceptions.Request_exist => new CustomException(System.Net.HttpStatusCode.Locked, "Прибор уже привязан"),
                InstrumentTracking.Domain.Exceptions.NotFound => NotFound(),
                InstrumentTracking.Domain.Exceptions.Ok => Ok(),
                _ => BadRequest(),
            };
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            return await _equipmentService.GetAllAsync();
        }

        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Equipment request)
        {
            var result = await _equipmentService.UpdateAsync(request, id);

            return result switch
            {
                InstrumentTracking.Domain.Exceptions.BadRequest => BadRequest(),
                InstrumentTracking.Domain.Exceptions.Request_exist => new CustomException(System.Net.HttpStatusCode.Locked, "Прибор уже привязан"),
                InstrumentTracking.Domain.Exceptions.NotFound => NotFound(),
                InstrumentTracking.Domain.Exceptions.Ok => Ok(),
                _ => BadRequest(),
            };
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetRequest(int id)
        {
            var request = await _equipmentService.GetAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }
    }
}
