using CustomerManagement.Api.Models;
using CustomerManagement.Api.Repositories.Interfaces;
using CustomerManagement.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;


namespace CustomerManagement.Api.Tests
{
    public class CustomerServiceTests
    {

        [Fact]
        public async Task GetByIdAsync_CustomerExists_ReturnsCustomer()
        {
            //Arrange
            var customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@example.com"
            };

            var mockRepository = new Mock<ICustomerRepository>();
            var mockLogger = new Mock<ILogger<CustomerService>>();

            mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(customer);

            var service = new CustomerService(
                mockRepository.Object,
                mockLogger.Object);

            //Act
            var result = await service.GetByIdAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John", result.FirstName);

        }



        [Fact]
        public async Task DeleteAsync_CustomerNotFound_ThrowsKeyNotFoundException()
        {
            //Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var mockLogger = new Mock<ILogger<CustomerService>>();

            mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Customer?)null);

            var service = new CustomerService(
                                mockRepository.Object,
                                mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => service.DeleteAsync(999));

        }



    }
}
