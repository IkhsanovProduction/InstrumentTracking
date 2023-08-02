using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InstrumentsTracking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using InstrumentTracking.Services;
using InstrumentTracking.Domain.Dtos;
using InstrumentsTracking.Domain.Models;
using InstrumentsTracking.Exception;
using InstrumentTracking.Infrastructure;

namespace InstrumentsTracking.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineersController : ControllerBase
    {
        private readonly IEngineerService _engineerService;

        public EngineersController(IEngineerService engineerService)
        {
            _engineerService = engineerService;
        }

        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> Post(Engineer engineer)
        {
            var result = await _engineerService.CreateAsync(engineer);

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
            var result = await _engineerService.DeleteAsync(id);

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
        public async Task<IEnumerable<Engineer>> GetAllAsync()
        {
            return await _engineerService.GetAllAsync();
        }

        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Engineer request)
        {
            var result = await _engineerService.UpdateAsync(request, id);

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
        public async Task<ActionResult<Engineer>> GetEngineer(int id)
        {
            return await _engineerService.GetAsync(id);
        }
    }
}
