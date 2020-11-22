using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
     public interface IFeeRepository
    {
        Task<IEnumerable<Fee>> ListAsync();
        Task<Fee> FindById(int id);
        Task AddAsync(Fee fee);
        void Update(Fee fee);
        void Remove(Fee fee);
    }
}
