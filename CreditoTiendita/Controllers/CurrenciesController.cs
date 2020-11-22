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
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly IMapper _mapper;

        public CurrenciesController(ICurrencyService currencyService, IMapper mapper)
        {
            _currencyService = currencyService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List Currencies",
            Description = "List of currencies",
            OperationId = "ListCurrencies",
            Tags = new[] { "currencies" }
            )]
        [HttpGet]
        public async Task<IEnumerable<CurrencyResource>> GetAllAsync()
        {
            var currencies = await _currencyService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Currency>, IEnumerable<CurrencyResource>>(currencies);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get a Currency",
           Description = "Get an specific Currency by id",
           OperationId = "GetCurrencyById",
           Tags = new[] { "currencies" }
           )]
        [HttpGet("{id}")]
        public async Task<CurrencyResource> GetByCurrencyId(int id)
        {
            var currency = await _currencyService.GetById(id);
            var resource = _mapper.Map<Currency, CurrencyResource>(currency.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create  a currency",
           Description = "Create a new currency",
           OperationId = "CreateCurrency",
           Tags = new[] { "currencies" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCurrencyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var currency = _mapper.Map<SaveCurrencyResource, Currency>(resource);

            var result = await _currencyService.SaveAsync(currency);

            if (!result.Success)
                return BadRequest(result.Message);

            var currencyResource = _mapper.Map<Currency, CurrencyResource>(currency);
            return Ok(currencyResource);
        }

        [SwaggerOperation(
           Summary = "Update currency",
           Description = "Update an specific currency",
           OperationId = "CreateCurrency",
           Tags = new[] { "currencies" }
           )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveCurrencyResource resource, int id)
        {
            var currency = _mapper.Map<SaveCurrencyResource, Currency>(resource);
            var result = await _currencyService.UpdateAsync(currency, id);

            if (!result.Success)
                return BadRequest(result.Message);
            var currencyResource = _mapper.Map<Currency, CurrencyResource>(result.Resource);
            return Ok(currencyResource);
        }

        [SwaggerOperation(
         Summary = "Delete currency",
         Description = "Delete an specific currency",
         OperationId = "DeleteCurrency",
         Tags = new[] { "currencies" }
         )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _currencyService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var currencyResource = _mapper.Map<Currency, CurrencyResource>(result.Resource);

            return Ok(currencyResource);
        }
    }
}
