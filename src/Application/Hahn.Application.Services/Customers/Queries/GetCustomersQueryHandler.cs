using Hahn.Application.Contract.Customers.Queries;
using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Services.Customers.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, ICollection<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ICollection<Customer>> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerListAsync();
        }
    }
}
