using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.Customer;

namespace TravelBookingPortal.Api.Services
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<Customer?> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);
        Task<Customer?> PatchCustomerAsync(int id, CustomerPatchDto patch);
        Task<bool> DeleteCustomerAsync(int id);
    }
}