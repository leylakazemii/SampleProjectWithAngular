using MediatR;

namespace Hahn.Application.Contract.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public Guid CustomerId { get; set; }
    }
}
