using AutoMapper;
using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services;
using CreditoTiendita.Extensions;
using CreditoTiendita.Resources;
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
    public class FeeTypesController : ControllerBase
    {
        private readonly IFeeTypeService _feeTypeService;
        private readonly IMapper _mapper;

        public FeeTypesController(IFeeTypeService feeTypeService, IMapper mapper)
        {
            _feeTypeService = feeTypeService;
            _mapper = mapper;
        }
        [SwaggerOperation(
             Summary = "List FeeType",
             Description = "List of FeeType",
             OperationId = "ListFeeType",
             Tags = new[] { "feeTypes" }
         )]
        [HttpGet]
        public async Task<IEnumerable<FeeTypeResource>> GetAll()
        {
            var feeTypes = await _feeTypeService.ListAsync();
            var resources = _mapper.Map<IEnumerable<FeeType>, IEnumerable<FeeTypeResource>>(feeTypes);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get a FeeType",
            Description = "Get an specific FeeType by id",
            OperationId = "GetFeeTypeById",
            Tags = new[] { "feeTypes" }
            )]
        [HttpGet("{id}")]
        public async Task<FeeTypeResource> GetByFeeTypeId(int id)
        {
            var feeType = await _feeTypeService.GetById(id);
            var resource = _mapper.Map<FeeType, FeeTypeResource>(feeType.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create  a FeeType",
           Description = "Create a new FeeType",
           OperationId = "CreateFeeType",
           Tags = new[] { "feeTypes" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveFeeTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var feeType = _mapper.Map<SaveFeeTypeResource, FeeType>(resource);
            var result = await _feeTypeService.SaveAsync(feeType);
            if (!result.Success)
                return BadRequest(result.Message);
            var feeTypeResource = _mapper.Map<FeeType, FeeTypeResource>(feeType);
            return Ok(feeTypeResource);
        }

        [SwaggerOperation(
           Summary = "Update FeeType",
           Description = "Update an specific FeeType",
           OperationId = "CreateFeeType",
           Tags = new[] { "feeTypes" }
           )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveFeeTypeResource resource, int id)
        {
            var feeType = _mapper.Map<SaveFeeTypeResource, FeeType>(resource);
            var result = await _feeTypeService.UpdateAsync(feeType, id);
            if (!result.Success)
                return BadRequest(result.Message);

            var feeTypeResource = _mapper.Map<FeeType, FeeTypeResource>(result.Resource);

            return Ok(feeTypeResource);
        }

        [SwaggerOperation(
         Summary = "Delete FeeType",
         Description = "Delete an specific FeeType",
         OperationId = "DeleteFeeType",
         Tags = new[] { "feeTypes" }
         )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _feeTypeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var feeTypeResource = _mapper.Map<FeeType, FeeTypeResource>(result.Resource);

            return Ok(feeTypeResource);
        }


    }
}
