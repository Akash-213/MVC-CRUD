using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Models.Domains;

namespace MVC_CRUD.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Employee> Employees { get; set; }

    }
}
