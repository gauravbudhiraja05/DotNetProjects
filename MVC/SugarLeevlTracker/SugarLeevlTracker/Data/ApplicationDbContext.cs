using System.Data.Entity;
using SugarLeevlTracker.Models;

namespace SugarLeevlTracker.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() :
          base("OktaConnectionString")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<SugarLevel> SugarLevels { get; set; }
    
}