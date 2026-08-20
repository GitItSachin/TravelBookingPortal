using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.Branch;

namespace TravelBookingPortal.Api.Repositories
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAllBranchesAsync();
        Task<Branch?> GetBranchByIdAsync(int id);
        Task<Branch> AddBranchAsync(Branch branch);
        Task<Branch?> UpdateBranchAsync(int id, Branch branch);
        Task<bool> DeleteBranchAsync(int id);
        Task<Branch?> GetBranchByCityAsync(string city);
        Task<Branch?> PatchBranchAsync(int id, BranchPatchDto patch);
    }
}