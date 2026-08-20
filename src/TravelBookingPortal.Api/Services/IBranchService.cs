using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.Branch;


namespace TravelBookingPortal.Api.Services
{
    public interface IBranchService
    {
        Task<IEnumerable<Branch>> GetAllBranchesAsync();
        Task<Branch?> GetBranchByIdAsync(int id);
        Task<bool> DeleteBranchAsync(int id);
        Task<Branch> CreateBranchAsync(CreateBranchDto createBranchDto);
        Task<Branch?> UpdateBranchAsync(int id, UpdateBranchDto updateBranchDto);
        Task<Branch?> PatchBranchAsync(int id, BranchPatchDto patch);
    }
}
