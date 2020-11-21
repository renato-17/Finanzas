using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class AccountResponse : BaseResponse<Account>
    {
        public AccountResponse(Account resource) : base(resource)
        {
        }

        public AccountResponse(string message) : base(message)
        {
        }

        public AccountResponse(Account resource, string message) : base(resource, message)
        {
        }
    }
}
