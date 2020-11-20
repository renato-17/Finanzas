using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface IFeeTypeService
    {
        Task<IEnumerable<FeeType>> ListAsync();
        Task<FeeTypeResponse> GetById(int id);
        Task<FeeTypeResponse> SaveAsync(FeeType feeType);
        Task<FeeTypeResponse> UpdateAsync(FeeType feeType, int id);
        Task<FeeTypeResponse> DeleteAsync(int id);
    }
}
