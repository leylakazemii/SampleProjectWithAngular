using Hahn.Application.Contract.Customers.Queries;
using Hahn.Application.Services.Customers.Queries;
using Hahn.Domain.Model.Customers;
using Moq;

namespace Hahn.Application.Services.Tests.Customers.Queries
{
    public class GetCustomersQueryHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public GetCustomersQueryHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handle_ReturnsListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
                new Customer { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "janedoe@example.com" }
            };

            _customerRepositoryMock.Setup(x => x.GetCustomerListAsync()).ReturnsAsync(customers);

            var query = new GetCustomersQuery();

            var handler = new GetCustomersQueryHandler(_customerRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(customers.Count, result.Count);
        }
    }
}