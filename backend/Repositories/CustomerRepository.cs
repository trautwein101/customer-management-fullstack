using CustomerManagement.Api.Data;
using CustomerManagement.Api.Models;
using CustomerManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {

            return await _context.Customers
                     .AsNoTracking()
                     .Where(c => c.IsActive)
                     .OrderBy(c => c.LastName)
                     .ToListAsync();

        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                            .AsNoTracking()
                            .Where(c => c.IsActive)
                            .FirstOrDefaultAsync(c => c.Id == id);

        }


        public async Task<Customer> AddAsync(Customer customer)
        {
            
            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return customer;
        }


        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();
        }


    }
}
