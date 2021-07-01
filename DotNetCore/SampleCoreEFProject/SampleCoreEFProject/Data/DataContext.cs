using Microsoft.EntityFrameworkCore;

namespace SampleCoreEFProject.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
