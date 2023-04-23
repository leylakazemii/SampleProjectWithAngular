using Hahn.Domain.Model.Customers;
using Hahn.Infrastructure.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Infrastructure.EfCore.Repository
{
    public class CustomerEfRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerEfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> AddCustomerAsync(Customer customers)
        {
            var result = _dbContext.Customers.Add(customers);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCustomerAsync(Guid Id)
        {
            var filteredData = _dbContext.Customers.Where(x => x.Id == Id).FirstOrDefault();
            _dbContext.Customers.Remove(filteredData);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid Id)
        {
            return await _dbContext.Customers.Where(x => x.Id == Id).FirstOrDefaultAsync()!;
        }

        public async Task<List<Customer>> GetCustomerListAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
