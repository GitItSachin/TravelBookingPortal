using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.Branch;
using TravelBookingPortal.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace TravelBookingPortal.Api.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _dbContext;

        public BranchRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Branch?> GetBranchByIdAsync(int id)
        {
            return await _dbContext.Branches.FindAsync(id);
        }

        public async Task<IEnumerable<Branch>> GetAllBranchesAsync()
        {
            return await _dbContext.Branches.ToListAsync();
        }

        public async Task<Branch> AddBranchAsync(Branch branch)
        {
            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch));
            }

            _dbContext.Branches.Add(branch);
            await _dbContext.SaveChangesAsync();
            return branch;
        }

        public async Task<Branch?> UpdateBranchAsync(int id, Branch branch)
        {
            var existingBranch = await _dbContext.Branches.FindAsync(id);
            if (existingBranch is null)
            {
                return null;
            }

            existingBranch.Name = branch.Name;
            existingBranch.City = branch.City;
            // Update other properties as needed

            await _dbContext.SaveChangesAsync();
            return existingBranch;
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            var existingBranch = await _dbContext.Branches.FindAsync(id);
            if (existingBranch is null)
            {
                return false;
            }

            _dbContext.Branches.Remove(existingBranch);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Branch?> GetBranchByCityAsync(string city)
        {
            return await _dbContext.Branches
                .Where(b => b.City.Contains(city))
                .FirstOrDefaultAsync();
        }

        public async Task<Branch?> PatchBranchAsync(int id, BranchPatchDto patch)
        {
            var existingBranch = await _dbContext.Branches.FindAsync(id);
            if (existingBranch is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(patch.Name))
            {
                existingBranch.Name = patch.Name;
            }

            if (!string.IsNullOrEmpty(patch.City))
            {
                existingBranch.City = patch.City;
            }

            await _dbContext.SaveChangesAsync();
            return existingBranch;
        }
    }
}