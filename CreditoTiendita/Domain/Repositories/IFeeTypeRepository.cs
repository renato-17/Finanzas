using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
    public interface IFeeTypeRepository
    {
        Task<IEnumerable<FeeType>> ListAsync();
        Task<FeeType> FindById(int id);
        Task AddAsync(FeeType feeType);
        void Update(FeeType feeType);
        void Remove(FeeType feeType);
    }
}
