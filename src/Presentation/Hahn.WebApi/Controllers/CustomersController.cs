using Hahn.Application.Contract.Customers.Commands;
using Hahn.Application.Contract.Customers.Queries;
using Hahn.Domain.Model.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Customer>> GetcustomerListAsync()
        {
            var Customer = await mediator.Send(new GetCustomersQuery());

            return Customer.ToList();
        }

        [HttpGet("customerId")]
        public async Task<Customer> GetcustomerByIdAsync(Guid customerId)
        {
            var Customer = await mediator.Send(new GetCustomerByIdQuery() { CustomerId = customerId });

            return Customer;
        }

        [HttpPost]
        public async Task<Customer> AddcustomerAsync(Customer customer)
        {
            try
            {

                var customerDetail = await mediator.Send(new CreateCustomerCommand() { Customer = customer });
                return customerDetail;
            }
            catch (Exception ex)
            {
                return new Customer();
            }
        }

        [HttpPut("{id}")]
        public async Task UpdatecustomerAsync(Guid id, Customer Customer)
        {
            try
            {
                await mediator.Send(new UpdateCustomerCommand() { CustomerId = id, Customer = Customer });
            }
            catch (Exception ex)
            {
            }
        }

        [HttpDelete("{id}")]
        public async Task DeletecustomerAsync(Guid id)
        {
            await mediator.Send(new DeleteCustomerCommand() { CustomerId = id });
        }
    }
}