using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class AccountStatusResponse : BaseResponse<AccountStatus>
    {
        public AccountStatusResponse(AccountStatus resource) : base(resource)
        {
        }

        public AccountStatusResponse(string message) : base(message)
        {
        }

        public AccountStatusResponse(AccountStatus resource, string message) : base(resource, message)
        {
        }
    }
}
