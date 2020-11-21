using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services.Communication
{
    public class ClientResponse : BaseResponse<Client>
    {
        public ClientResponse(Client resource) : base(resource)
        {
        }

        public ClientResponse(string message) : base(message)
        {
        }

        public ClientResponse(Client resource, string message) : base(resource, message)
        {
        }
    }
}
