using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class VendorsRepository : IRepository<Vendors>
    {
        private DataContext _context;

        public VendorsRepository(DataContext data)
        {
            _context = data;
        }

        public async Task<List<Vendors>> Get()
        {
            return await _context.Vendors.ToListAsync();
        }

        public async Task<Vendors> Post(Vendors vendors)
        {
            _context.Vendors.Add(vendors);
            await _context.SaveChangesAsync();

            return vendors;
        }

        public async Task<Vendors?> Put(int id, Vendors vendors)
        {
            var rep = await _context.Vendors.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(rep == null)
                return null;

            _context.Entry(vendors).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return vendors;
        }

        public async Task<Vendors?> Delete(int id)
        {
            var rep = await _context.Vendors.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (rep == null)
                return null;

            _context.Vendors.Remove(rep);
            await _context.SaveChangesAsync();

            return rep;
        }
    }
}
