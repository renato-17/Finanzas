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
    [Route("api/accounts/{accountId}[controller]")]
    [ApiController]
    public class AccountStatusController : ControllerBase
    {
        private readonly IAccountStatusService _accountStatusService;
        private readonly IMapper _mapper;

        public AccountStatusController(IAccountStatusService accountStatusService, IMapper mapper)
        {
            _accountStatusService = accountStatusService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List AccountStatuses",
            Description = "List of accounts",
            OperationId = "ListAccountStatuses",
            Tags = new[] { "accountStatuses" }
            )]
        [HttpGet]
        public async Task<IEnumerable<AccountStatusResource>> GetAllAsync(int accountId)
        {
            var accountStatuses = await _accountStatusService.ListByAccountIdAsync(accountId);
            var resources = _mapper.Map<IEnumerable<AccountStatus>, IEnumerable<AccountStatusResource>>(accountStatuses);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get an AccountStatus",
           Description = "Get an specific AccountStatus by id",
           OperationId = "GetAccountStatusById",
           Tags = new[] { "accountStatuses" }
           )]
        [HttpGet("{accountStatusId}")]
        public async Task<AccountStatusResource> GetByAccountId(int accountId, int accountStatusId)
        {
            var accountStatus = await _accountStatusService.GetByIdAndAccountId(accountId, accountStatusId);
            var resource = _mapper.Map<AccountStatus, AccountStatusResource>(accountStatus.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create an accountStatus",
           Description = "Create a new accountStatus",
           OperationId = "CreateAccountStatus",
           Tags = new[] { "accountStatuses" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAccountStatusResource resource, int accountId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var accountStatus = _mapper.Map<SaveAccountStatusResource, AccountStatus>(resource);

            var result = await _accountStatusService.SaveAsync(accountId, accountStatus);

            if (!result.Success)
                return BadRequest(result.Message);

            var accountStatusResource = _mapper.Map<AccountStatus, AccountStatusResource>(accountStatus);
            return Ok(accountStatusResource);
        }

        [SwaggerOperation(
           Summary = "Update accountStatus",
           Description = "Update an specific accountStatus",
           OperationId = "UpdateAccountStatus",
           Tags = new[] { "accountStatuses" }
           )]
        [HttpPut("{accountStatusId}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveAccountStatusResource resource, int accountId, int accountStatusId)
        {
            var accountStatus = _mapper.Map<SaveAccountStatusResource, AccountStatus>(resource);
            var result = await _accountStatusService.UpdateAsync(accountId, accountStatusId, accountStatus);

            if (!result.Success)
                return BadRequest(result.Message);
            var accountStatusResource = _mapper.Map<AccountStatus, AccountStatusResource>(result.Resource);
            return Ok(accountStatusResource);
        }

        [SwaggerOperation(
         Summary = "Delete accountStatus",
         Description = "Delete an specific accountStatus",
         OperationId = "DeleteAccountStatus",
         Tags = new[] { "accountStatuses" }
         )]
        [HttpDelete("accountStatusId")]
        public async Task<IActionResult> DeleteAsync(int accountId, int accountStatusId)
        {
            var result = await _accountStatusService.DeleteAsync(accountId, accountStatusId);

            if (!result.Success)
                return BadRequest(result.Message);

            var accountStatusResource = _mapper.Map<AccountStatus, AccountStatusResource>(result.Resource);

            return Ok(accountStatusResource);
        }
    }
}
