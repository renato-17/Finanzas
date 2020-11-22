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
    public class AditionalCostRepository : BaseRepository, IAditionalCostRepository
    {
        public AditionalCostRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(AditionalCost account)
        {
            await _context.AditionalCosts.AddAsync(account);
        }

        public async Task<AditionalCost> FindByIdAndAccountId(int accountId, int id)
        {
            return await _context.AditionalCosts
                .Where(ad => (ad.Id == id) && (ad.AccountId == accountId))
                .FirstAsync();
        }

        public async Task<IEnumerable<AditionalCost>> ListByAccountIdAsync(int accountId)
        {
            return await _context.AditionalCosts
                .Where(ad => ad.AccountId == accountId)
                .ToListAsync();
        }

        public void Remove(AditionalCost account)
        {
            _context.AditionalCosts.Remove(account);
        }

        public void Update(AditionalCost account)
        {
            _context.AditionalCosts.Update(account);
        }
    }
}
