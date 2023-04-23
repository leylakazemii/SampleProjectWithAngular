using Hahn.Domain.Model.Customers;
using Hahn.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hahn.Infrastructure.EfCore.Context
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options,
            IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add any additional configuration for your entities here
            modelBuilder.ApplyConfiguration(new CustomerDbMap());
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
