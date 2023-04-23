using Hahn.Application.Contract.Customers.Commands;
using Hahn.Application.Services.Customers.Commands;
using Hahn.Domain.Model.Customers;
using Moq;

namespace Hahn.Application.Services.Tests.Customers.Commands
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public CreateCustomerCommandHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handle_ValidCustomer_ReturnsCreatedCustomer()
        {
            // Arrange
            var customer = new Customer { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
            _customerRepositoryMock.Setup(x => x.AddCustomerAsync(It.IsAny<Customer>())).ReturnsAsync(customer);
            var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object);
            var command = new CreateCustomerCommand { Customer = customer };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(customer.Id, result.Id);
            Assert.Equal(customer.FirstName, result.FirstName);
            Assert.Equal(customer.LastName, result.LastName);
            _customerRepositoryMock.Verify(x => x.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }
    }
}