using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class TransactionResponse: BaseResponse<Transaction>
    {
        public TransactionResponse(Transaction resource) : base(resource)
        {

        }
        public TransactionResponse(string message) : base(message)
        {

        }
    }
}
