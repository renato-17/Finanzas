using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> ListAsync();
        Task<Account> FindByClientId(int id);
        Task AddAsync(Account account);
        void Update(Account account);
        void Remove(Account account);
    }
}
