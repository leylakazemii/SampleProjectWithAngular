using Hahn.Domain.Model.Customers;
using Xunit;
namespace Hahn.Domain.Test
{
    public class CustomerValidatorTests
    {
        private readonly CustomerValidator _validator;

        public CustomerValidatorTests()
        {
            _validator = new CustomerValidator();
        }

        [Fact]
        public void Should_Pass_When_Valid_Input()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "+12345678901",
                BankAccountNumber = "1234567890"
            };

            // Act
            var result = _validator.Validate(customer);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Fail_When_FirstName_Is_Empty()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "+12345678901",
                BankAccountNumber = "1234567890"
            };

            // Act
            var result = _validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'First Name' is required.", result.Errors.Single().ErrorMessage);
            Assert.Equal(nameof(customer.FirstName), result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Should_Fail_When_LastName_Is_Null()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = null,
                DateOfBirth = new DateTime(1985, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "+12345678901",
                BankAccountNumber = "1234567890"
            };

            // Act
            var result = _validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'Last Name' is required.", result.Errors.Single().ErrorMessage);
            Assert.Equal(nameof(customer.LastName), result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Should_Fail_When_DateOfBirth_Is_In_The_Future()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now.AddDays(1),
                Email = "john.doe@example.com",
                PhoneNumber = "+12345678901",
                BankAccountNumber = "1234567890"
            };

            // Act
            var result = _validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'Date Of Birth' must be at least 18 years old.", result.Errors[0].ErrorMessage);
            Assert.Equal("'Date Of Birth' must be in the past.", result.Errors[1].ErrorMessage);
            Assert.Equal(nameof(customer.DateOfBirth), result.Errors[1].PropertyName);
        }

        [Fact]
        public void Should_Fail_When_Email_Is_Invalid()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                Email = "not_an_email_address",
                PhoneNumber = "+12345678901",
                BankAccountNumber = "1234567890"
            };

            // Act
            var result = _validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'Email' is not a valid email address.", result.Errors.Single().ErrorMessage);
            Assert.Equal(nameof(customer.Email), result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Should_Fail_When_PhoneNumber_Is_Too_Long()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "+12345678901234567890",
                BankAccountNumber = "1234567890"
            };

            // Act
            var result = _validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'Phone Number' is not a valid phone number.", result.Errors[0].ErrorMessage);
            Assert.Equal("'Phone Number' must not exceed 15 characters.", result.Errors[1].ErrorMessage);
            Assert.Equal(nameof(customer.PhoneNumber), result.Errors[0].PropertyName);
        }

        [Fact]
        public void Should_Fail_When_BankAccountNumber_Is_Too_Short()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "+12345678901",
                BankAccountNumber = "12345"
            };

            // Act
            var result = _validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'Bank Account Number' must be at least 6 characters long.", result.Errors.Single().ErrorMessage);
            Assert.Equal(nameof(customer.BankAccountNumber), result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Should_Fail_When_BankAccountNumber_Is_Too_Long()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "+12345678901",
                BankAccountNumber = "12345678901234567890"
            };

            // Act
            var result = _validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'Bank Account Number' must not exceed 15 characters.", result.Errors.Single().ErrorMessage);
            Assert.Equal(nameof(customer.BankAccountNumber), result.Errors.Single().PropertyName);
        }
    }
}