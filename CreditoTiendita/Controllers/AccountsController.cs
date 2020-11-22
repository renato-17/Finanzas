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
    [Route("api/clients/{clientId}/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        //[SwaggerOperation(
        //    Summary = "List Accounts",
        //    Description = "List of accounts",
        //    OperationId = "ListAccountss",
        //    Tags = new[] { "accounts" }
        //    )]
        //[HttpGet]
        //public async Task<IEnumerable<AccountResource>> GetAllAsync()
        //{
        //    var accounts = await _accountService.ListAsync();
        //    var resources = _mapper.Map<IEnumerable<Account>, IEnumerable<AccountResource>>(accounts);
        //    return resources;
        //}

        [SwaggerOperation(
           Summary = "Get an Account",
           Description = "Get an specific Account by id",
           OperationId = "GetAccountById",
           Tags = new[] { "accounts" }
           )]
        [HttpGet]
        public async Task<AccountResource> GetByClientId(string clientId)
        {
            var account = await _accountService.GetByClientId(clientId);
            var resource = _mapper.Map<Account, AccountResource>(account.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create an account",
           Description = "Create a new account",
           OperationId = "CreateAccount",
           Tags = new[] { "accounts" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAccountResource resource, string clientId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var account = _mapper.Map<SaveAccountResource, Account>(resource);

            var result = await _accountService.SaveAsync(clientId,resource.CurrencyId,resource.PeriodId,account);

            if (!result.Success)
                return BadRequest(result.Message);

            var accountResource = _mapper.Map<Account, AccountResource>(account);
            return Ok(accountResource);
        }

        [SwaggerOperation(
           Summary = "Update account",
           Description = "Update an specific account",
           OperationId = "CreateAccount",
           Tags = new[] { "accounts" }
           )]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] SaveAccountResource resource, string clientId)
        {
            var account = _mapper.Map<SaveAccountResource, Account>(resource);
            var result = await _accountService.UpdateAsync(clientId, resource.CurrencyId, resource.PeriodId, account);

            if (!result.Success)
                return BadRequest(result.Message);
            var accountResource = _mapper.Map<Account, AccountResource>(result.Resource);
            return Ok(accountResource);
        }

        [SwaggerOperation(
         Summary = "Delete account",
         Description = "Delete an specific account",
         OperationId = "DeleteAccount",
         Tags = new[] { "accounts" }
         )]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string clientId)
        {
            var result = await _accountService.DeleteAsync(clientId);

            if (!result.Success)
                return BadRequest(result.Message);

            var accountResource = _mapper.Map<Account, AccountResource>(result.Resource);

            return Ok(accountResource);
        }
    }
}
