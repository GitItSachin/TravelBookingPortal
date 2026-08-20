using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Repositories;
using TravelBookingPortal.Api.Dtos.Branch;
namespace TravelBookingPortal.Api.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public Task<IEnumerable<Branch>> GetAllBranchesAsync()
            => _branchRepository.GetAllBranchesAsync();

        public Task<Branch?> GetBranchByIdAsync(int id)
            => _branchRepository.GetBranchByIdAsync(id);

        public Task<bool> DeleteBranchAsync(int id)
            => _branchRepository.DeleteBranchAsync(id);

        public Task<Branch?> PatchBranchAsync(int id, BranchPatchDto patch)
            => _branchRepository.PatchBranchAsync(id, patch);
        
        public async Task<Branch> CreateBranchAsync(CreateBranchDto createBranchDto)
        {
            var branch = new Branch
            {
                Name = createBranchDto.Name,
                City = createBranchDto.City,
                // Map other properties as needed
            };

            return await _branchRepository.AddBranchAsync(branch);
        }

        public async Task<Branch?> UpdateBranchAsync(int id, UpdateBranchDto updateBranchDto)
            {
                var branch = new Branch
                {
                    Name = updateBranchDto.Name,
                    City = updateBranchDto.City
                };

                return await _branchRepository.UpdateBranchAsync(id, branch);
            }
    }
}
