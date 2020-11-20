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
    public class PeriodRepository : BaseRepository, IPeriodRepository
    {
        public PeriodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Period period)
        {
            await _context.Periods.AddAsync(period);
        }

        public async Task<Period> FindById(int id)
        {
            return await _context.Periods.FindAsync(id);
        }

        public async Task<IEnumerable<Period>> ListAsync()
        {
            return await _context.Periods.ToListAsync();
        }

        public void Remove(Period period)
        {
            _context.Periods.Remove(period);
        }

        public void Update(Period period)
        {
            _context.Periods.Update(period);
        }
    }
}
