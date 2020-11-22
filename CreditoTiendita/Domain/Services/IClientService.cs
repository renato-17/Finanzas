using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> ListAsync();
        Task<ClientResponse> GetById(string id);
        Task<ClientResponse> SaveAsync(Client client);
        Task<ClientResponse> UpdateAsync(Client client, string id);
        Task<ClientResponse> DeleteAsync(string id);
    }
}
