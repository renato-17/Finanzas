using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services;
using CreditoTiendita.Extensions;
using CreditoTiendita.Resources;
using CreditoTiendita.Resources.SaveResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditoTiendita.Controllers
{
    [Produces("application/json")]
    [Route("api/accounts/{accountId}/[controller]")]
    [ApiController]
    public class AditionalCostController : ControllerBase
    {
        private readonly IAditionalCostService _aditionalCostService;
        private readonly IMapper _mapper;

        public AditionalCostController(IAditionalCostService aditionalCostService, IMapper mapper)
        {
            _aditionalCostService = aditionalCostService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List AditionalCosts",
            Description = "List of accounts",
            OperationId = "ListAditionalCosts",
            Tags = new[] { "aditionalCosts" }
            )]
        [HttpGet]
        public async Task<IEnumerable<AditionalCostResource>> GetAllAsync(int accountId)
        {
            var aditionalCosts = await _aditionalCostService.ListByAccountIdAsync(accountId);
            var resources = _mapper.Map<IEnumerable<AditionalCost>, IEnumerable<AditionalCostResource>>(aditionalCosts);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get an AditionalCost",
           Description = "Get an specific AditionalCost by id",
           OperationId = "GetAditionalCostById",
           Tags = new[] { "aditionalCosts" }
           )]
        [HttpGet("{aditionalCostId}")]
        public async Task<AditionalCostResource> GetByAccountId(int accountId, int aditionalCostId)
        {
            var aditionalCost = await _aditionalCostService.GetByIdAndAccountId(accountId,aditionalCostId);
            var resource = _mapper.Map<AditionalCost, AditionalCostResource>(aditionalCost.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create an aditionalCost",
           Description = "Create a new aditionalCost",
           OperationId = "CreateAditionalCost",
           Tags = new[] { "aditionalCosts" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAditionalCostResource resource, int accountId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var aditionalCost = _mapper.Map<SaveAditionalCostResource, AditionalCost>(resource);

            var result = await _aditionalCostService.SaveAsync(accountId, aditionalCost);

            if (!result.Success)
                return BadRequest(result.Message);

            var aditionalCostResource = _mapper.Map<AditionalCost, AditionalCostResource>(aditionalCost);
            return Ok(aditionalCostResource);
        }

        [SwaggerOperation(
           Summary = "Update aditionalCost",
           Description = "Update an specific aditionalCost",
           OperationId = "UpdateAditionalCost",
           Tags = new[] { "aditionalCosts" }
           )]
        [HttpPut("{aditionalCostId}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveAditionalCostResource resource, int accountId, int aditionalCostId)
        {
            var aditionalCost = _mapper.Map<SaveAditionalCostResource, AditionalCost>(resource);
            var result = await _aditionalCostService.UpdateAsync(accountId, aditionalCostId, aditionalCost);

            if (!result.Success)
                return BadRequest(result.Message);
            var aditionalCostResource = _mapper.Map<AditionalCost, AditionalCostResource>(result.Resource);
            return Ok(aditionalCostResource);
        }

        [SwaggerOperation(
         Summary = "Delete aditionalCost",
         Description = "Delete an specific aditionalCost",
         OperationId = "DeleteAditionalCost",
         Tags = new[] { "aditionalCosts" }
         )]
        [HttpDelete("aditionalCostId")]
        public async Task<IActionResult> DeleteAsync(int accountId, int aditionalCostId)
        {
            var result = await _aditionalCostService.DeleteAsync(accountId, aditionalCostId);

            if (!result.Success)
                return BadRequest(result.Message);

            var aditionalCostResource = _mapper.Map<AditionalCost, AditionalCostResource>(result.Resource);

            return Ok(aditionalCostResource);
        }
    }
}
