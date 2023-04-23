using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Contract.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid CustomerId { get; set; }
    }
}
