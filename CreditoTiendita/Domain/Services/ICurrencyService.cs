using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> ListAsync();
        Task<CurrencyResponse> GetById(int id);
        Task<CurrencyResponse> SaveAsync(Currency currency);
        Task<CurrencyResponse> UpdateAsync(Currency currency, int id);
        Task<CurrencyResponse> DeleteAsync(int id);
    }
}
