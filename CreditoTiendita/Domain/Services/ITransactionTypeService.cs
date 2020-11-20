using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface ITransactionTypeService
    {
        Task<IEnumerable<TransactionType>> ListAsync();
        Task<TransactionTypeResponse> GetById(int id);
        Task<TransactionTypeResponse> SaveAsync(TransactionType transactionType);
        Task<TransactionTypeResponse> UpdateAsync(TransactionType transactionType, int id);
        Task<TransactionTypeResponse> DeleteAsync(int id);
    }
}
