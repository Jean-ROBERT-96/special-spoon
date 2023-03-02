using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User?> Delete(int id)
        {
            var result = await _dataContext.Users.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (result == null)
                return null;

            _dataContext.Users.Remove(result);
            await _dataContext.SaveChangesAsync();
            return result;
        }

        public async Task<List<User>> Get()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User> Post(User objects)
        {
            _dataContext.Users.Add(objects);
            await _dataContext.SaveChangesAsync();
            return objects;
        }

        public async Task<User?> Put(int id, User objects)
        {
            var result = await _dataContext.Users.Where(o => o.Id == id).FirstOrDefaultAsync();
            if(result == null)
                return null;

            _dataContext.Attach(objects).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return objects;
        }
    }
}
