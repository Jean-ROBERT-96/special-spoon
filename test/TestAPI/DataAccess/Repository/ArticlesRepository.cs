using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ArticlesRepository : IRepository<Articles>
    {
        DataContext _context;

        public ArticlesRepository(DataContext data)
        {
            _context = data;
        }

        public async Task<List<Articles>> Get()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Articles> Post(Articles articles)
        {
            _context.Articles.Add(articles);
            await _context.SaveChangesAsync();

            return articles;
        }

        public async Task<Articles?> Put(int id, Articles articles)
        {
            var rep = await _context.Articles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (rep == null)
                return null;

            _context.Attach(articles).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return articles;
        }

        public async Task<Articles?> Delete(int id)
        {
            var rep = await _context.Articles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (rep == null)
                return null;

            _context.Articles.Remove(rep);
            await _context.SaveChangesAsync();

            return rep;
        }
    }
}
