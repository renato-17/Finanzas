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
    public class ClientRepository : BaseRepository, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        public async Task<Client> FindById(string id)
        {
            return await _context.Clients
                .Where(a => a.Dni == id)
                .Include(a => a.Account)
                .FirstAsync();
        }

        public async Task<IEnumerable<Client>> ListAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public void Remove(Client client)
        {
            _context.Clients.Remove(client);
        }

        public void Update(Client client)
        {
            _context.Clients.Update(client);
        }
    }
}
