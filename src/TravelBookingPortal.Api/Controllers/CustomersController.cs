using TravelBookingPortal.Api.Dtos.Customer;
using TravelBookingPortal.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer is null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            var created = await _customerService.CreateCustomerAsync(createCustomerDto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            var updated = await _customerService.UpdateCustomerAsync(id, updateCustomerDto);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Customer>> PatchCustomer(int id, [FromBody] CustomerPatchDto patch)
        {
            var updated = await _customerService.PatchCustomerAsync(id, patch);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteCustomerAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}