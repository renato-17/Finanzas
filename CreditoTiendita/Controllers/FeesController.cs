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
    [Route("api/accounts/{accountId}/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly IFeeService _feeService;
        private readonly IMapper _mapper;

        public FeesController(IFeeService feeService, IMapper mapper)
        {
            _feeService = feeService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List Fees",
            Description = "List of fees",
            OperationId = "ListFees",
            Tags = new[] { "fees" }
            )]
        [HttpGet]
        public async Task<IEnumerable<FeeResource>> GetAllAsync()
        {
            var fees = await _feeService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Fee>, IEnumerable<FeeResource>>(fees);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get a Fee",
           Description = "Get an specific Fee by id",
           OperationId = "GetFeeById",
           Tags = new[] { "fees" }
           )]
        [HttpGet("{id}")]
        public async Task<FeeResource> GetByFeeId(int id)
        {
            var fee = await _feeService.GetById(id);
            var resource = _mapper.Map<Fee, FeeResource>(fee.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create  a fee",
           Description = "Create a new fee",
           OperationId = "CreateFee",
           Tags = new[] { "fees" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveFeeResource resource, int accountId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var fee = _mapper.Map<SaveFeeResource, Fee>(resource);

            var result = await _feeService.SaveAsync(fee, resource.FeeTypeId, accountId);
            if (!result.Success)
                return BadRequest(result.Message);
            var feeResource = _mapper.Map<Fee, FeeResource>(fee);
            return Ok(feeResource);
        }

        [SwaggerOperation(
           Summary = "Update fee",
           Description = "Update an specific fee",
           OperationId = "UpdateFee",
           Tags = new[] { "fees" }
           )]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] SaveFeeResource resource, int accountId)
        {
            var fee = _mapper.Map<SaveFeeResource, Fee>(resource);
            var result = await _feeService.UpdateAsync(fee, accountId);

            if (!result.Success)
                return BadRequest(result.Message);
            var feeResource = _mapper.Map<Fee, FeeResource>(result.Resource);
            return Ok(feeResource);
        }

        [SwaggerOperation(
         Summary = "Delete fee",
         Description = "Delete an specific fee",
         OperationId = "DeleteFee",
         Tags = new[] { "fees" }
         )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _feeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var feeResource = _mapper.Map<Fee, FeeResource>(result.Resource);

            return Ok(feeResource);
        }
    }
}
