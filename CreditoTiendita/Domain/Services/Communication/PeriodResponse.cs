using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class PeriodResponse : BaseResponse<Period>
    {
        public PeriodResponse(Period resource) : base(resource)
        {

        }
        public PeriodResponse(string message) : base(message)
        {

        }
    }
}
