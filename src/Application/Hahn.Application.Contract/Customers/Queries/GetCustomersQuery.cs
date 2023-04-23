using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Contract.Customers.Queries
{
    public class GetCustomersQuery : IRequest<ICollection<Customer>>
    {
    }
}
