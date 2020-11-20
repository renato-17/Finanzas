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
    public class FeeRepository : BaseRepository, IFeeRepository
    {
        public FeeRepository(AppDbContext context): base(context)
        {

        }
        public async Task AddAsync(Fee fee)
        {
            await _context.Fees.AddAsync(fee);
        }

        public async Task<Fee> FindById(int id)
        {
            return await _context.Fees.FindAsync(id);
        }

        public async Task<IEnumerable<Fee>> ListAsync()
        {
            return await _context.Fees.ToListAsync();
        }

        public void Remove(Fee fee)
        {
            _context.Fees.Remove(fee);
        }

        public void Update(Fee fee)
        {
            _context.Fees.Update(fee);
        }
    }
}
