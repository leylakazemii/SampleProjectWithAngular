using Hahn.Application.Contract.Customers.Queries;
using Hahn.Domain.Model.Customers;
using MediatR;

namespace Hahn.Application.Services.Customers.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerByIdAsync(query.CustomerId);
        }
    }
}
