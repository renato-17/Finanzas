using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface IAditionalCostService
    {
        Task<IEnumerable<AditionalCost>> ListByAccountIdAsync(int accountId);
        Task<AditionalCostResponse> GetByIdAndAccountId(int accountId, int id);
        Task<AditionalCostResponse> SaveAsync(int accountId, AditionalCost aditionalCost);
        Task<AditionalCostResponse> UpdateAsync(int accountId, int id, AditionalCost aditionalCost);
        Task<AditionalCostResponse> DeleteAsync(int accountId, int id);
    }
}
