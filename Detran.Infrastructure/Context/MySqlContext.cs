using Detran.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace Detran.Infrastructure.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MySqlContext).GetType().Assembly);
        }

        public DbSet<ApiUserRole> ApiUser { get; set; }
        public DbSet<ApiUserRole> ApiUserRole { get; set; }
    }
}