using CustomerManagement.Api.Models;
using CustomerManagement.Api.Repositories.Interfaces;
using CustomerManagement.Api.Services.Interfaces;

namespace CustomerManagement.Api.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;


        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }


        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            try
            {
                _logger.LogInformation("Creating a new customer");

                var savedCustomer = await _customerRepository.AddAsync(customer);

                _logger.LogInformation(
                    "Customer {CustomerId} created successfully",
                    savedCustomer.Id);

                return savedCustomer;

            }
            catch(Exception)
            {
                _logger.LogWarning(
                    "Customer creation failed; propagating exception");

                throw;
            }
            finally
            {
                _logger.LogDebug(
                    "Customer creation attempt finished");
            }

        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {

            _logger.LogInformation("Attempting to delete customer {CustomerId}", id);

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                _logger.LogWarning("Customer {CustomerId} was not found", id);

                throw new KeyNotFoundException($"Customer with ID {id} was not found.");
            }

            await _customerRepository.DeleteAsync(customer);

            _logger.LogInformation("Customer {CustomerId} delete successfuly", id);
        }

    }
}
