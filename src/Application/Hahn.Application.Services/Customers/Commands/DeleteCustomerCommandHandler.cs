using Hahn.Application.Contract.Customers.Commands;
using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Services.Customers.Commands
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var studentDetails = await _customerRepository.GetCustomerByIdAsync(command.CustomerId);
            if (studentDetails == null)
                throw new ArgumentException();

            await _customerRepository.DeleteCustomerAsync(studentDetails.Id);
        }
    }
}
