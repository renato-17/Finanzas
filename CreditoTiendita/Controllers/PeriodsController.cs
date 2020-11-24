using AutoMapper;
using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services;
using CreditoTiendita.Extensions;
using CreditoTiendita.Resources;
using CreditoTiendita.Resources.SaveResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodsController : ControllerBase
    {
        private readonly IPeriodService _periodService;
        private readonly IMapper _mapper;

        public PeriodsController(IPeriodService periodService, IMapper mapper)
        {
            _periodService = periodService;
            _mapper = mapper;
        }

        [SwaggerOperation(
             Summary = "List Period",
             Description = "List of Period",
             OperationId = "ListPeriod",
             Tags = new[] { "periods" }
         )]
        [HttpGet]
        public async Task<IEnumerable<PeriodResource>> GetAll()
        {
            var periods = await _periodService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Period>, IEnumerable<PeriodResource>>(periods);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get a Period",
            Description = "Get an specific Period by id",
            OperationId = "GetPeriodById",
            Tags = new[] { "periods" }
            )]
        [HttpGet("{id}")]
        public async Task<PeriodResource> GetByPeriodId(int id)
        {
            var period = await _periodService.GetById(id);
            var resource = _mapper.Map<Period, PeriodResource>(period.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create  a Period",
           Description = "Create a new Period",
           OperationId = "CreatePeriod",
           Tags = new[] { "periods" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePeriodResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var period = _mapper.Map<SavePeriodResource, Period>(resource);
            var result = await _periodService.SaveAsync(period);
            if (!result.Success)
                return BadRequest(result.Message);
            var periodResource = _mapper.Map<Period, PeriodResource>(period);
            return Ok(periodResource);
        }

        [SwaggerOperation(
           Summary = "Update Period",
           Description = "Update an specific Period",
           OperationId = "UpdatePeriod",
           Tags = new[] { "periods" }
           )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SavePeriodResource resource, int id)
        {
            var period = _mapper.Map<SavePeriodResource, Period>(resource);
            var result = await _periodService.UpdateAsync(period, id);
            if (!result.Success)
                return BadRequest(result.Message);

            var periodResource = _mapper.Map<Period, PeriodResource>(result.Resource);

            return Ok(periodResource);
        }

        [SwaggerOperation(
         Summary = "Delete Period",
         Description = "Delete an specific Period",
         OperationId = "DeletePeriod",
         Tags = new[] { "periods" }
         )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _periodService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var periodResource = _mapper.Map<Period, PeriodResource>(result.Resource);

            return Ok(periodResource);
        }

    }
}
