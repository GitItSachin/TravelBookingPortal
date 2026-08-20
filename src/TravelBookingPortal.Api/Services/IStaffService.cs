using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.Staff;

namespace TravelBookingPortal.Api.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllStaffAsync();
        Task<Staff?> GetStaffByIdAsync(int id);
        Task<Staff> CreateStaffAsync(CreateStaffDto createStaffDto);
        Task<Staff?> UpdateStaffAsync(int id, UpdateStaffDto updateStaffDto);
        Task<bool> DeleteStaffAsync(int id);
        Task<Staff?> PatchStaffAsync(int id, StaffPatchDto patch);
    }
}
