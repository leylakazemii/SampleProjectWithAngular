using Hahn.Application.Contract.Customers.Commands;
using Hahn.Application.Contract.Customers.Queries;
using Hahn.Domain.Model.Customers;
using Hahn.WebApi.Controllers;
using MediatR;
using Moq;
using Xunit;

namespace Hahn.WebApi.Tests
{
    public class CustomersControllerTests
    {
        private readonly CustomersController _customersController;
        private readonly Mock<IMediator> _mediatorMock;

        public CustomersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _customersController = new CustomersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetcustomerListAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>()
            {
                new Customer { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
                new Customer { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe" }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customers);

            // Act
            var result = await _customersController.GetcustomerListAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customers.Count, result.Count);
            Assert.Equal(customers.First().Id, result.First().Id);
            Assert.Equal(customers.Last().Id, result.Last().Id);
        }

        [Fact]
        public async Task GetcustomerByIdAsync_ShouldReturnCustomerWithGivenId()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer { Id = customerId, FirstName = "John", LastName = "Doe" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            // Act
            var result = await _customersController.GetcustomerByIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer.Id, result.Id);
            Assert.Equal(customer.FirstName, result.FirstName);
            Assert.Equal(customer.LastName, result.LastName);
        }

        [Fact]
        public async Task AddcustomerAsync_ShouldReturnCreatedCustomer()
        {
            // Arrange
            var customer = new Customer { FirstName = "John", LastName = "Doe" };
            var createdCustomer = new Customer { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdCustomer);

            // Act
            var result = await _customersController.AddcustomerAsync(customer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdCustomer.Id, result.Id);
            Assert.Equal(createdCustomer.FirstName, result.FirstName);
            Assert.Equal(createdCustomer.LastName, result.LastName);
        }

        [Fact]
        public async Task UpdatecustomerAsync_ShouldUpdateCustomerWithGivenId()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer { Id = customerId, FirstName = "John", LastName = "Doe" };

            // Act
            await _customersController.UpdatecustomerAsync(customerId, customer);

            // Assert
            _mediatorMock.Verify(m => m.Send(It.Is<UpdateCustomerCommand>(c => c.CustomerId == customerId && c.Customer == customer), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeletecustomerAsync_WithValidId_ShouldCallMediatorWithCorrectCommand()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var controller = new CustomersController(_mediatorMock.Object);

            // Act
            await controller.DeletecustomerAsync(customerId);

            // Assert
            _mediatorMock.Verify(x => x.Send(It.Is<DeleteCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}