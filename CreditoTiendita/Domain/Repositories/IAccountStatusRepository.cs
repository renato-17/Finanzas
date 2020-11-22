using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
    public interface IAccountStatusRepository
    {
        Task<IEnumerable<AccountStatus>> ListByAccountIdAsync(int accountId);
        Task<AccountStatus> FindByIdAndAccountId(int accountId, int id);
        Task AddAsync(AccountStatus accountStatus);
        void Update(AccountStatus accountStatus);
        void Remove(AccountStatus accountStatus);
    }
}
