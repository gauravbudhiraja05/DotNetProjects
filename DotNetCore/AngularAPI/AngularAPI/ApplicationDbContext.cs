using AngularAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace AngularAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public virtual DbSet<StudentEntity> students { get; set; }
    }
}
