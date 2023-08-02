using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InstrumentsTracking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using InstrumentTracking.Services.Interfaces;
using InstrumentsTracking.Exception;

namespace InstrumentsTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplingActsController : ControllerBase
    {
        private readonly ISamplingActService _samplingActService;

        public SamplingActsController(ISamplingActService samplingActService)
        {
            _samplingActService = samplingActService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> Post(SamplingAct engineer)
        {
            var result = await _samplingActService.CreateAsync(engineer);

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
            var result = await _samplingActService.DeleteAsync(id);

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
        public async Task<IEnumerable<object>> GetAllAsync()
        {
            return await _samplingActService.GetAllAsync();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SamplingAct request)
        {
            var result = await _samplingActService.UpdateAsync(request, id);

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
        public async Task<ActionResult<SamplingAct>> GetRequest(int id)
        {
            var request = await _samplingActService.GetAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }
    }
}
