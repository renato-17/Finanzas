using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface IFeeService
    {
        Task<IEnumerable<Fee>> ListAsync();
        Task<FeeResponse> GetById(int id);
        Task<FeeResponse> SaveAsync(Fee fee, int feeTypeId);
        Task<FeeResponse> UpdateAsync(Fee fee, int id);
        Task<FeeResponse> DeleteAsync(int id);
    }
}
