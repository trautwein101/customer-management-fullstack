using CustomerManagement.Api.Models;
using CustomerManagement.Api.Repositories.Interfaces;
using CustomerManagement.Api.Services.Interfaces;

namespace CustomerManagement.Api.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;


        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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
            return await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} was not found.");
            }

            await _customerRepository.DeleteAsync(customer);
        }

    }
}
