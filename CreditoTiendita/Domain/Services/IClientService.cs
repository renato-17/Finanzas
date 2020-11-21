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
        Task<ClientResponse> GetById(int id);
        Task<ClientResponse> SaveAsync(Client client);
        Task<ClientResponse> UpdateAsync(Client client, int id);
        Task<ClientResponse> DeleteAsync(int id);
    }
}
