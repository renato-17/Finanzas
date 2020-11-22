using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
    public interface IAditionalCostRepository
    {
        Task<IEnumerable<AditionalCost>> ListByAccountIdAsync(int accountId);
        Task<AditionalCost> FindByIdAndAccountId(int accountId,int id);
        Task AddAsync(AditionalCost account);
        void Update(AditionalCost account);
        void Remove(AditionalCost account);
    }
}
