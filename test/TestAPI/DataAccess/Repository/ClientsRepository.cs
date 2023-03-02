using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ClientsRepository : IRepository<Clients>
    {
        private DataContext _context;

        public ClientsRepository(DataContext data)
        {
            _context = data;
        }

        public async Task<List<Clients>> Get()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Clients> Post(Clients clients)
        {
            _context.Clients.Add(clients);
            await _context.SaveChangesAsync();

            return clients;
        }

        public async Task<Clients?> Put(int id, Clients clients)
        {
            var rep = await _context.Clients.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(rep == null)
                return null;

            _context.Entry(clients).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return clients;
        }

        public async Task<Clients?> Delete(int id)
        {
            var rep = await _context.Clients.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (rep == null)
                return null;

            _context.Clients.Remove(rep);
            await _context.SaveChangesAsync();

            return rep;
        }
    }
}
