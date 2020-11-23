using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services;
using CreditoTiendita.Domain.Services.Communication;
using CreditoTiendita.Extensions;
using CreditoTiendita.Resources;
using CreditoTiendita.Resources.SaveResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditoTiendita.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "Pay debts",
            Description = "Pay all debts",
            OperationId = "PayDebts",
            Tags = new[] { "Payment" }
            )]
        [Route("api/clients/{clientId}/payment")]
        [HttpPost]
        public async Task<IActionResult> payDebts([FromBody] SaveTransactionResource  resource, string clientId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var transaction = _mapper.Map<SaveTransactionResource, Transaction>(resource);

            var result = await _paymentService.PayDebt(transaction,clientId, resource.TransactionTypeId);
            if (!result.Succesfull)
                return BadRequest(result.Message);

            var transactionResource = _mapper.Map<Transaction, TransactionResource>(transaction);
            return Ok(transactionResource);
        }

        [SwaggerOperation(
            Summary = "List Transactions of account status",
            Description = "List of Transactions of account status",
            OperationId = "ListTransactionsByAccountStatus",
            Tags = new[] { "Payment" }
            )]
        [Route("api/clients/{clientId}/account-status/{statusId}")]
        [HttpGet]
        public async Task<IEnumerable<TransactionResource>> GetAllAsync(string clientId, int statusId)
        {
            var transactions = await _paymentService.GenerateAccountStatus(statusId,clientId);
            var resources = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionResource>>(transactions);
            return resources;
        }

    }
}
