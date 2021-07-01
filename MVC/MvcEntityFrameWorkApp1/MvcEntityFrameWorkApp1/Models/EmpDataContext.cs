using System.Data.Entity;

namespace MvcEntityFrameWorkApp1.Models
{
    public class EmpDataContext : DbContext
    {
        public EmpDataContext()
          : base("name=MySqlConnection")
        {
        }

        public DbSet<Employee> employees { get; set; }
    }
}