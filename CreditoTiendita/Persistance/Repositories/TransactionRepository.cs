using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Persistance.Context;
using CreditoTiendita.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Persistance.Repositories
{
    public class TransactionRepository: BaseRepository, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {

        }
        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task<Transaction> FindById(int id)
        {
            return await _context.Transactions
                .Where(t => t.Id == id)
                .Include(t => t.TransactionType)
                .Include(t => t.Account)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> ListAsync()
        {
            return await _context.Transactions.Include(t=>t.TransactionType).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> ListByAccountIdAsync(int accountId)
        {
            return await _context.Transactions
             .Where(tr => tr.AccountId == accountId)
             .Include(t=>t.TransactionType)
             .ToListAsync();
        }

        public void Remove(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
        }

        public void Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
        }
    }
}
