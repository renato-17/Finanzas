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
    public class TransactionTypesController : ControllerBase
    {
        private readonly ITransactionTypeService _transactionTypeService;
        private readonly IMapper _mapper;

        public TransactionTypesController(ITransactionTypeService transactionTypeService, IMapper mapper)
        {
            _transactionTypeService = transactionTypeService;
            _mapper = mapper;
        }
        [SwaggerOperation(
             Summary = "List TransactionType",
             Description = "List of TransactionType",
             OperationId = "ListTransactionType",
             Tags = new[] { "transactionTypes" }
         )]
        [HttpGet]
        public async Task<IEnumerable<TransactionTypeResource>> GetAll()
        {
            var transactionTypes = await _transactionTypeService.ListAsync();
            var resources = _mapper.Map<IEnumerable<TransactionType>, IEnumerable<TransactionTypeResource>>(transactionTypes);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get a TransactionType",
            Description = "Get an specific TransactionType by id",
            OperationId = "GetTransactionTypeById",
            Tags = new[] { "transactionTypes" }
            )]
        [HttpGet("{id}")]
        public async Task<TransactionTypeResource> GetByTransactionTypeId(int id)
        {
            var transactionType = await _transactionTypeService.GetById(id);
            var resource = _mapper.Map<TransactionType, TransactionTypeResource>(transactionType.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create  a TransactionType",
           Description = "Create a new TransactionType",
           OperationId = "CreateTransactionType",
           Tags = new[] { "transactionTypes" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveTransactionTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var transactionType = _mapper.Map<SaveTransactionTypeResource, TransactionType>(resource);
            var result = await _transactionTypeService.SaveAsync(transactionType);
            if (!result.Success)
                return BadRequest(result.Message);
            var transactionTypeResource = _mapper.Map<TransactionType, TransactionTypeResource>(transactionType);
            return Ok(transactionTypeResource);
        }

        [SwaggerOperation(
           Summary = "Update TransactionType",
           Description = "Update an specific TransactionType",
           OperationId = "CreateTransactionType",
           Tags = new[] { "transactionTypes" }
           )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveTransactionTypeResource resource, int id)
        {
            var transactionType = _mapper.Map<SaveTransactionTypeResource, TransactionType>(resource);
            var result = await _transactionTypeService.UpdateAsync(transactionType, id);
            if (!result.Success)
                return BadRequest(result.Message);

            var transactionTypeResource = _mapper.Map<TransactionType, TransactionTypeResource>(result.Resource);

            return Ok(transactionTypeResource);
        }

        [SwaggerOperation(
         Summary = "Delete TransactionType",
         Description = "Delete an specific TransactionType",
         OperationId = "DeleteTransactionType",
         Tags = new[] { "transactionTypes" }
         )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _transactionTypeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var transactionTypeResource = _mapper.Map<TransactionType, TransactionTypeResource>(result.Resource);

            return Ok(transactionTypeResource);
        }
    }
}
