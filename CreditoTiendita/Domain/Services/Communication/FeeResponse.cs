using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class FeeResponse : BaseResponse<Fee>
    {
        public FeeResponse(Fee resource) : base(resource)
        {

        }
        public FeeResponse(string message) : base(message)
        {

        }
    }
}
