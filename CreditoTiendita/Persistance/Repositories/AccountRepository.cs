﻿using CreditoTiendita.Domain.Models;
using CreditoTiendita.Domain.Persistance.Context;
using CreditoTiendita.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Persistance.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public async Task<Account> FindByClientId(int id)
        {
            return await _context.Accounts
                .Where(a => a.ClientId == id)
                .Include(a=>a.Currency)
                .Include(a=>a.Fee)
                .Include(a=>a.Period)
                .FirstAsync();
        }

        public async Task<IEnumerable<Account>> ListAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public void Remove(Account account)
        {
            _context.Accounts.Remove(account);
        }

        public void Update(Account account)
        {
            _context.Accounts.Update(account);
        }
    }
}