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
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List Currencies",
            Description = "List of clients",
            OperationId = "ListCurrencies",
            Tags = new[] { "clients" }
            )]
        [HttpGet]
        public async Task<IEnumerable<ClientResource>> GetAllAsync()
        {
            var clients = await _clientService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientResource>>(clients);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get a Client",
           Description = "Get an specific Client by id",
           OperationId = "GetClientById",
           Tags = new[] { "clients" }
           )]
        [HttpGet("{id}")]
        public async Task<ClientResource> GetByClientId(int id)
        {
            var client = await _clientService.GetById(id);
            var resource = _mapper.Map<Client, ClientResource>(client.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create  a client",
           Description = "Create a new client",
           OperationId = "CreateClient",
           Tags = new[] { "clients" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveClientResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var client = _mapper.Map<SaveClientResource, Client>(resource);

            var result = await _clientService.SaveAsync(client);

            if (!result.Success)
                return BadRequest(result.Message);

            var clientResource = _mapper.Map<Client, ClientResource>(client);
            return Ok(clientResource);
        }

        [SwaggerOperation(
           Summary = "Update client",
           Description = "Update an specific client",
           OperationId = "CreateClient",
           Tags = new[] { "clients" }
           )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveClientResource resource, int id)
        {
            var client = _mapper.Map<SaveClientResource, Client>(resource);
            var result = await _clientService.UpdateAsync(client, id);

            if (!result.Success)
                return BadRequest(result.Message);
            var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);
            return Ok(clientResource);
        }

        [SwaggerOperation(
         Summary = "Delete client",
         Description = "Delete an specific client",
         OperationId = "DeleteClient",
         Tags = new[] { "clients" }
         )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _clientService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

            return Ok(clientResource);
        }
    }
}
