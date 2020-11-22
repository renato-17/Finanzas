using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface IAccountStatusService
    {
        Task<IEnumerable<AccountStatus>> ListByAccountIdAsync(int accountId);
        Task<AccountStatusResponse> GetByIdAndAccountId(int accountId, int id);
        Task<AccountStatusResponse> SaveAsync(int accountId, AccountStatus accountStatus);
        Task<AccountStatusResponse> UpdateAsync(int accountId, int id, AccountStatus accountStatus);
        Task<AccountStatusResponse> DeleteAsync(int accountId, int id);
    }
}
