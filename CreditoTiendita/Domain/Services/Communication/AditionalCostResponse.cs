using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class AditionalCostResponse : BaseResponse<AditionalCost>
    {
        public AditionalCostResponse(AditionalCost resource) : base(resource)
        {
        }

        public AditionalCostResponse(string message) : base(message)
        {
        }

        public AditionalCostResponse(AditionalCost resource, string message) : base(resource, message)
        {
        }
    }
}
