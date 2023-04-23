using Hahn.Application.Contract.Customers.Commands;
using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Services.Customers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {

        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);
            if (customer == null)
            {
                throw new EntryPointNotFoundException();
            }

            customer.BankAccountNumber = request.Customer.BankAccountNumber;
            customer.DateOfBirth = request.Customer.DateOfBirth;
            customer.Email = request.Customer.Email;
            customer.FirstName = request.Customer.FirstName;
            customer.LastName = request.Customer.LastName;
            customer.PhoneNumber = request.Customer.PhoneNumber;

            await _customerRepository.UpdateCustomerAsync(customer);
        }
    }
}
