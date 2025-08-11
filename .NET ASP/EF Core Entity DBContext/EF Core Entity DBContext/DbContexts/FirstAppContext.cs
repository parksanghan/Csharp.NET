using EF_Core_Entity_DBContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_Entity_DBContext.DbContexts
{
    public class FirstAppContext : DbContext
    {
        public FirstAppContext(DbContextOptions<FirstAppContext> options)
            : base(options) { }

        public DbSet<LogHistory> LogHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogHistory>()
                        .Property(x => x.Seq)
                        .ValueGeneratedOnAdd();
        }
    }
}
