using CreditoTiendita.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Repositories
{
     public interface ITransactionTypeRepository
    {
        Task<IEnumerable<TransactionType>> ListAsync();
        Task<TransactionType> FindById(int id);
        Task AddAsync(TransactionType transactionType);
        void Update(TransactionType transactionType);
        void Remove(TransactionType transactionType);
    }
}
