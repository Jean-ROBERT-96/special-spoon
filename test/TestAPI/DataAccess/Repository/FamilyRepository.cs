using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class FamilyRepository : IRepository<Family>
    {
        private DataContext _context;

        public FamilyRepository(DataContext data)
        {
            _context = data;
        }

        public async Task<List<Family>> Get()
        {
            return await _context.Families.ToListAsync();
        }

        public async Task<Family> Post(Family family)
        {
            _context.Families.Add(family);
            await _context.SaveChangesAsync();

            return family;
        }
        
        public async Task<Family?> Put(int id, Family family)
        {
            var rep = await _context.Families.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (rep == null)
                return null;

            _context.Entry(family).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return family;
        }

        public async Task<Family?> Delete(int id)
        {
            var rep = await _context.Families.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (rep == null)
                return null;

            _context.Families.Remove(rep);
            await _context.SaveChangesAsync();

            return rep;
        }
    }
}
