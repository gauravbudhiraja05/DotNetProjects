using EmployeeServer.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeServer.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }

    }
}
