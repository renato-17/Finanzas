using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> ListAsync();
        Task<IEnumerable<Transaction>> ListByAccountIdAsync(int accountId);
        Task<Transaction> FindById(int id);
        Task AddAsync(Transaction transaction);
        void Update(Transaction transaction);
        void Remove(Transaction transaction);
    }
}
