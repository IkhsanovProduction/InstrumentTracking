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
using InstrumentsTracking.Exception;
using InstrumentTracking.Services;
using static InstrumentTracking.Infrastructure.Repositories.RequestRepository;
using InstrumentTracking.Domain.Dtos;

namespace InstrumentsTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestsService _requestService;

        public RequestsController(IRequestsService requestsService)
        {
            _requestService = requestsService;
        }

        // GET: api/Requests
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IEnumerable<object>> GetRequest()
        {
            return await _requestService.GetAllAsync();
        }

        // GET: api/Requests/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _requestService.GetAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            var result = await _requestService.UpdateAsync(request, id);

            return result switch
            {
                InstrumentTracking.Domain.Exceptions.BadRequest => BadRequest(),
                InstrumentTracking.Domain.Exceptions.Request_exist => new CustomException(System.Net.HttpStatusCode.Locked, "Прибор уже привязан"),
                InstrumentTracking.Domain.Exceptions.NotFound => NotFound(),
                InstrumentTracking.Domain.Exceptions.Ok => Ok(),
                _ => BadRequest(),
            };
        }

        // POST: api/Requests

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> PostRequest(Request request)
        {
            var result = await _requestService.CreateAsync(request);

            return result switch
            {
                InstrumentTracking.Domain.Exceptions.BadRequest => BadRequest(),
                InstrumentTracking.Domain.Exceptions.Request_exist => new CustomException(System.Net.HttpStatusCode.Locked, "Прибор уже привязан"),
                InstrumentTracking.Domain.Exceptions.NotFound => NotFound(),
                InstrumentTracking.Domain.Exceptions.Ok => Ok(),
                _ => BadRequest(),
            };
        }

        // DELETE: api/Requests/5
        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task DeleteRequest(int id)
        {
            await _requestService.DeleteAsync(id);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("/LinkedRequests")]
        public async Task<IEnumerable<object>> GetAllRequestLinked()
        {
            return await _requestService.GetAllLinkedAsync();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("/LinkedRequests/{id}")]
        public async Task<IEnumerable<object>> GetRequestLinked(int id)
        {
            return await _requestService.GetLinkedAsync(id);
        }
    }
}
