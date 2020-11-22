using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class FeeTypeResponse: BaseResponse<FeeType>
    {
        public FeeTypeResponse(FeeType resource): base(resource)
        {

        }
        public FeeTypeResponse(string message): base(message)
        {

        }
    }
}
