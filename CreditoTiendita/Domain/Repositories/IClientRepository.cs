using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> ListAsync();
        Task<Client> FindById(string id);
        Task AddAsync(Client client);
        void Update(Client client);
        void Remove(Client client);
    }
}
