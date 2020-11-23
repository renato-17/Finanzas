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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List Transactions",
            Description = "List of Transactions",
            OperationId = "ListTransactions",
            Tags = new[] { "Transactions" }
            )]
        [HttpGet]
        public async Task<IEnumerable<TransactionResource>> GetAllAsync(int accountId)
        {
            var transactions = await _transactionService.ListByAccountIdAsync(accountId);
            var resources = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionResource>>(transactions);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get a Transaction",
           Description = "Get an specific Transaction by id",
           OperationId = "GetTransactionById",
           Tags = new[] { "Transactions" }
           )]
        [HttpGet("{id}")]
        public async Task<TransactionResource> GetByTransactionId(int id)
        {
            var transaction = await _transactionService.GetById(id);
            var resource = _mapper.Map<Transaction, TransactionResource>(transaction.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create  a Transaction",
           Description = "Create a new Transaction",
           OperationId = "CreateTransaction",
           Tags = new[] { "Transactions" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveTransactionResource resource, int accountId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var transaction = _mapper.Map<SaveTransactionResource, Transaction>(resource);
            var result = await _transactionService.SaveAsync(transaction, resource.TransactionTypeId, accountId);
            if (!result.Success)
                return BadRequest(result.Message);
            var transactionResource = _mapper.Map<Transaction, TransactionResource>(transaction);
            return Ok(transactionResource);
        }

        [SwaggerOperation(
           Summary = "Update Transaction",
           Description = "Update an specific Transaction",
           OperationId = "CreateTransaction",
           Tags = new[] { "Transactions" }
           )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveTransactionResource resource, int id)
        {
            var transaction = _mapper.Map<SaveTransactionResource, Transaction>(resource);
            var result = await _transactionService.UpdateAsync(transaction, id);

            if (!result.Success)
                return BadRequest(result.Message);
            var transactionResource = _mapper.Map<Transaction, TransactionResource>(result.Resource);
            return Ok(transactionResource);
        }

        [SwaggerOperation(
         Summary = "Delete Transaction",
         Description = "Delete an specific Transaction",
         OperationId = "DeleteTransaction",
         Tags = new[] { "Transactions" }
         )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _transactionService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var transactionResource = _mapper.Map<Transaction, TransactionResource>(result.Resource);

            return Ok(transactionResource);
        }
    }
}
