using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface IPeriodService
    {
        Task<IEnumerable<Period>> ListAsync();
        Task<PeriodResponse> GetById(int id);
        Task<PeriodResponse> SaveAsync(Period period);
        Task<PeriodResponse> UpdateAsync(Period period, int id);
        Task<PeriodResponse> DeleteAsync(int id);
    }
}
