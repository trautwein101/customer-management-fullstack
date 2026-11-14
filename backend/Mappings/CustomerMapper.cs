using CustomerManagement.Api.DTOs;
using CustomerManagement.Api.Models;

namespace CustomerManagement.Api.Mappings
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                IsActive = customer.IsActive,
                CreatedAt = customer.CreatedAt
            };
        }

        public static Customer ToEntity(CreateCustomerDto dto)
        {
            return new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
        }


        public static void UpdateEntity(
            UpdateCustomerDto dto,
            Customer customer)
        {
            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.IsActive = dto.IsActive;
        }

    }
}
