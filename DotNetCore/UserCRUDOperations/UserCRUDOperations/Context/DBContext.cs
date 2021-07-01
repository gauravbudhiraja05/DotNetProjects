using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserCRUDOperations.Models;

namespace UserCRUDOperations.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
    }
}
