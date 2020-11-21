using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class CurrencyResponse : BaseResponse<Currency>
    {
        public CurrencyResponse(Currency resource) : base(resource)
        {
        }

        public CurrencyResponse(string message) : base(message)
        {
        }

        public CurrencyResponse(Currency resource, string message) : base(resource, message)
        {
        }
    }
}
