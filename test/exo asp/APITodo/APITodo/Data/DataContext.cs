using APITodo.Models;
using Microsoft.EntityFrameworkCore;

namespace APITodo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Tasks> Tasks1 { get; set; }
    }
}
