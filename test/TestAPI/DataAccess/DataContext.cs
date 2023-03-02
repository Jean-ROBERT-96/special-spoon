using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }

        //Articles
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Family> Families { get; set; }
        //Entités
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Vendors> Vendors { get; set; }
    }
}
