using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> ListAsync();
        Task<TransactionResponse> GetById(int id);
        Task<TransactionResponse> SaveAsync(Transaction transaction, int transactionTypeId);
        Task<TransactionResponse> UpdateAsync(Transaction Transaction, int id);
        Task<TransactionResponse> DeleteAsync(int id);
    }
}
