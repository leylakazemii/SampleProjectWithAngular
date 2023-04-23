using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Contract.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
