using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
    public interface IPeriodRepository
    {
        Task<IEnumerable<Period>> ListAsync();
        Task<Period> FindById(int id);
        Task AddAsync(Period feeType);
        void Update(Period feeType);
        void Remove(Period feeType);
    }
}
