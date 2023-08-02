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
    public class RequestsHistoryController : ControllerBase
    {
        private readonly IRequestHistoryService _requestService;

        public RequestsHistoryController(IRequestHistoryService requestsService)
        {
            _requestService = requestsService;
        }

        // GET: api/Requests
        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IEnumerable<object>> GetRequest()
        {
            return await _requestService.GetAllAsync();
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("/RequestsHistoryNonLinked")]
        public async Task<IEnumerable<object>> GetAllRequestNonLinked()
        {
            return await _requestService.GetAllLinkedAsync();
        }

        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _requestService.DeleteAsync(id);

            return result switch
            {
                InstrumentTracking.Domain.Exceptions.BadRequest => BadRequest(),
                InstrumentTracking.Domain.Exceptions.NotFound => NotFound(),
                InstrumentTracking.Domain.Exceptions.Ok => Ok(),
                _ => BadRequest(),
            };
        }
    }
}
