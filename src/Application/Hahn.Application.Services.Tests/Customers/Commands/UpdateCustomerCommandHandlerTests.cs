using Hahn.Application.Contract.Customers.Commands;
using Hahn.Application.Services.Customers.Commands;
using Hahn.Domain.Model.Customers;
using Moq;

namespace Hahn.Application.Services.Tests.Customers.Commands
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public UpdateCustomerCommandHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handle_WithValidCustomer_ShouldUpdateCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerToUpdate = new Customer
            {
                BankAccountNumber = "123456789",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890"
            };
            var updateCustomerCommand = new UpdateCustomerCommand
            {
                CustomerId = customerId,
                Customer = customerToUpdate
            };
            var handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object);
            _customerRepositoryMock
                .Setup(x => x.GetCustomerByIdAsync(customerId))
                .ReturnsAsync(new Customer());

            // Act
            await handler.Handle(updateCustomerCommand, CancellationToken.None);

            // Assert
            _customerRepositoryMock.Verify(x => x.UpdateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithInvalidCustomer_ShouldThrowEntryPointNotFoundException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerToUpdate = new Customer
            {
                BankAccountNumber = "123456789",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890"
            };
            var updateCustomerCommand = new UpdateCustomerCommand
            {
                CustomerId = customerId,
                Customer = customerToUpdate
            };
            var handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object);
            _customerRepositoryMock
                .Setup(x => x.GetCustomerByIdAsync(customerId))
                .ReturnsAsync((Customer)null);

            // Act + Assert
            await Assert.ThrowsAsync<EntryPointNotFoundException>(() => handler.Handle(updateCustomerCommand, CancellationToken.None));
        }
    }
}