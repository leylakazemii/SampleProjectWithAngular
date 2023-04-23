using Hahn.Application.Contract.Customers.Commands;
using Hahn.Application.Services.Customers.Commands;
using Hahn.Domain.Model.Customers;
using Moq;

namespace Hahn.Application.Services.Tests.Customers.Commands
{
    public class DeleteCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public DeleteCustomerCommandHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handle_CustomerExists_DeleteCustomer()
        {
            // Arrange
            var customer = new Customer { Id = Guid.NewGuid() };
            _customerRepositoryMock.Setup(x => x.GetCustomerByIdAsync(customer.Id)).ReturnsAsync(customer);
            var handler = new DeleteCustomerCommandHandler(_customerRepositoryMock.Object);

            // Act
            await handler.Handle(new DeleteCustomerCommand { CustomerId = customer.Id }, CancellationToken.None);

            // Assert
            _customerRepositoryMock.Verify(x => x.DeleteCustomerAsync(customer.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_CustomerDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var nonExistingCustomerId = Guid.NewGuid();
            _customerRepositoryMock.Setup(x => x.GetCustomerByIdAsync(nonExistingCustomerId)).ReturnsAsync(null as Customer);
            var handler = new DeleteCustomerCommandHandler(_customerRepositoryMock.Object);

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(new DeleteCustomerCommand { CustomerId = nonExistingCustomerId }, CancellationToken.None));
        }
    }
}