using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Contract.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}
