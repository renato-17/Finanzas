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
    public class TransactionTypeRepository: BaseRepository, ITransactionTypeRepository
    {
        public TransactionTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(TransactionType transactionType)
        {
            await _context.TransactionTypes.AddAsync(transactionType);
        }

        public async Task<TransactionType> FindById(int id)
        {
            return await _context.TransactionTypes.FindAsync(id);
        }

        public async Task<IEnumerable<TransactionType>> ListAsync()
        {
            return await _context.TransactionTypes.ToListAsync();
        }

        public void Remove(TransactionType transactionType)
        {
            _context.TransactionTypes.Remove(transactionType);
        }

        public void Update(TransactionType transactionType)
        {
            _context.TransactionTypes.Update(transactionType);
        }
    }
}
