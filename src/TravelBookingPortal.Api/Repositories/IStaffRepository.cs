using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Repositories
{
    public interface IStaffRepository
    {
        Task<Staff?> GetStaffByIdAsync(int id);
        Task<Staff?> GetStaffByEmailAsync(string email);
        Task<IEnumerable<Staff>> GetAllStaffAsync();
        Task<Staff> AddStaffAsync(Staff staff);
        Task<Staff?> UpdateStaffAsync(int id, Staff staff);
        Task<bool> DeleteStaffAsync(int id);
    }
}
