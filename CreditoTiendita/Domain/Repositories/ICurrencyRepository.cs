using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> ListAsync();
        Task<Currency> FindById(int id);
        Task AddAsync(Currency currency);
        void Update(Currency currency);
        void Remove(Currency currency);
    }
}
