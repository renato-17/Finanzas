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
    public class AccountStatusRepository : BaseRepository, IAccountStatusRepository
    {
        public AccountStatusRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(AccountStatus accountStatus)
        {
            await _context.AccountStatuses.AddAsync(accountStatus);
        }

        public async Task<AccountStatus> FindByIdAndAccountId(int accountId, int id)
        {
            return await _context.AccountStatuses
                .Where(ad => (ad.Id == id) && (ad.AccountId == accountId))
                .FirstAsync();
        }

        public async Task<IEnumerable<AccountStatus>> ListByAccountIdAsync(int accountId)
        {
            return await _context.AccountStatuses
                 .Where(ad => ad.AccountId == accountId)
                 .ToListAsync();
        }

        public void Remove(AccountStatus accountStatus)
        {
            _context.AccountStatuses.Remove(accountStatus);
        }

        public void Update(AccountStatus accountStatus)
        {
            _context.AccountStatuses.Update(accountStatus);
        }
    }
}
