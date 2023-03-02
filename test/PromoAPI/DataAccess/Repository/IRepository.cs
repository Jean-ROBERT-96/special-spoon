using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRepository<T>
    {
        Task<List<T>> Get();
        Task<T> Post(T objects);
        Task<T?> Put(int id, T objects);
        Task<T?> Delete(int id);
    }
}
