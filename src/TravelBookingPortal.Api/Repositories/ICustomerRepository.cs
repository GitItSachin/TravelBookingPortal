using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer?> UpdateCustomerAsync(int id, Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
