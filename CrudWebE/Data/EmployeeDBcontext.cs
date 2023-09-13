using CrudWebE.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudWebE.Data
{
    public class EmployeeDBcontext : DbContext
    {
        public EmployeeDBcontext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employeecs> Employees { get; set; }

        public DbSet<CrudWebE.Models.UpdateEmployee>? UpdateEmployee { get; set; }

      
    }
}
