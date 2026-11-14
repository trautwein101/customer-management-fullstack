using CustomerManagement.Api.DTOs;
using CustomerManagement.Api.Mappings;
using CustomerManagement.Api.Models;
using CustomerManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {

        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAllAsync()
        {

            var customers = await _customerService.GetAllAsync();

            var customerDtos = customers
                                .Select(c => CustomerMapper.ToDto(c))
                                .ToList();

            return Ok(customerDtos);

        }

        [HttpGet("{id:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<CustomerDto>> GetByIdAsync(int id)
        {

            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = CustomerMapper.ToDto(customer);
            return Ok(customerDto);

        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> AddAsync([FromBody] CreateCustomerDto dto)
        {
            var customer = CustomerMapper.ToEntity(dto);

            var savedCustomer = await _customerService.AddAsync(customer);

            var customerDto = CustomerMapper.ToDto(savedCustomer);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = customerDto.Id },
                customerDto);

        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerDto dto, int id)
        {

            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            CustomerMapper.UpdateEntity(dto, customer);

            await _customerService.UpdateAsync(customer);

            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _customerService.DeleteAsync(id);

            return NoContent();

        }




    }
}
