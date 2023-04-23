using Hahn.Application.Contract.Customers.Commands;
using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Services.Customers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {

        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                BankAccountNumber = command.Customer.BankAccountNumber,
                DateOfBirth = command.Customer.DateOfBirth,
                Email = command.Customer.Email,
                FirstName = command.Customer.FirstName,
                LastName = command.Customer.LastName,
                PhoneNumber = command.Customer.PhoneNumber,

            };

            return await _customerRepository.AddCustomerAsync(customer);
        }
    }
}
