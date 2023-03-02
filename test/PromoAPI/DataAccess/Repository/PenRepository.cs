using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PenRepository : IRepository<Pen>
    {
        private readonly DataContext _dataContext;

        public PenRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Pen?> Delete(int id)
        {
            var result = await _dataContext.Pens.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (result == null)
                return null;

            _dataContext.Pens.Remove(result);
            await _dataContext.SaveChangesAsync();
            return result;
        }

        public async Task<List<Pen>> Get()
        {
            return await _dataContext.Pens.ToListAsync();
        }

        public async Task<Pen> Post(Pen objects)
        {
            _dataContext.Pens.Add(objects);
            await _dataContext.SaveChangesAsync();
            return objects;
        }

        public async Task<Pen?> Put(int id, Pen objects)
        {
            var result = await _dataContext.Pens.Where(o => o.Id == id).FirstOrDefaultAsync();
            if(result == null)
                return null;

            _dataContext.Attach(objects).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return objects;
        }
    }
}
