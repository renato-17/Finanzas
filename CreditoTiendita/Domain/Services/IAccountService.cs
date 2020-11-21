using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> ListAsync();
        Task<AccountResponse> GetByClientId(int clientId);
        Task<AccountResponse> SaveAsync(int clientId,int currencyId, int periodId, Account account);
        Task<AccountResponse> UpdateAsync(int clientId, int currencyId, int periodId, Account account);
        Task<AccountResponse> DeleteAsync(int clientId);
    }
}
