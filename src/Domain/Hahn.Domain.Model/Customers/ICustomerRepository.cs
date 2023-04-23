namespace Hahn.Domain.Model.Customers
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetCustomerListAsync();
        public Task<Customer> GetCustomerByIdAsync(Guid Id);
        public Task<Customer> AddCustomerAsync(Customer customer);
        public Task UpdateCustomerAsync(Customer customer);
        public Task DeleteCustomerAsync(Guid Id);
    }
}