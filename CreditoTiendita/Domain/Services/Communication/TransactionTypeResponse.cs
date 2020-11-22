using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class TransactionTypeResponse: BaseResponse<TransactionType>
    {
        public TransactionTypeResponse(TransactionType resource) : base(resource)
        {

        }
        public TransactionTypeResponse(string message) : base(message)
        {

        }
    }
}
