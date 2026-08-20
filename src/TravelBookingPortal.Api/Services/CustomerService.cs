using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.Customer;
using TravelBookingPortal.Api.Repositories;

namespace TravelBookingPortal.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
            => await _customerRepository.GetCustomerByIdAsync(id);

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
            => await _customerRepository.GetAllCustomersAsync();

        public async Task<Customer> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = new Customer
            {
                FullName = createCustomerDto.FullName,
                Email = createCustomerDto.Email,
                Phone = createCustomerDto.Phone
            };

            return await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<Customer?> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = new Customer
            {
                FullName = updateCustomerDto.FullName,
                Email = updateCustomerDto.Email,
                Phone = updateCustomerDto.Phone
            };

            return await _customerRepository.UpdateCustomerAsync(id, customer);
        }

        public async Task<Customer?> PatchCustomerAsync(int id, CustomerPatchDto patch)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
                return null;

            if (patch.FullName != null)
                existingCustomer.FullName = patch.FullName;
            if (patch.Email != null)
                existingCustomer.Email = patch.Email;
            if (patch.Phone != null)
                existingCustomer.Phone = patch.Phone;

            return await _customerRepository.UpdateCustomerAsync(id, existingCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
            => await _customerRepository.DeleteCustomerAsync(id);
    }
}