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
    public class FeeTypeRepository : BaseRepository, IFeeTypeRepository
    {
        public FeeTypeRepository(AppDbContext context) : base(context)
        {

        }

        public async Task AddAsync(FeeType feeType)
        {
            await _context.FeeTypes.AddAsync(feeType);
        }

        public async Task<FeeType> FindById(int id)
        {
            return await _context.FeeTypes.FindAsync(id);
        }

        public async Task<IEnumerable<FeeType>> ListAsync()
        {
            return await _context.FeeTypes.ToListAsync();
        }

        public void Remove(FeeType feeType)
        {
            _context.FeeTypes.Remove(feeType);
        }

        public void Update(FeeType feeType)
        {
            _context.FeeTypes.Update(feeType);
        }
    }
}
