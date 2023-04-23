using Hahn.Domain.Model.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hahn.Infrastructure.EfCore.Repository.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(DbContextOptions<DbContextClass> options,
            IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add any additional configuration for your entities here
            modelBuilder.ApplyConfiguration(new CustomerDbMap());
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
