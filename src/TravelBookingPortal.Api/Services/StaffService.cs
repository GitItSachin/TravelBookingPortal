using Microsoft.AspNetCore.Identity;
using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Repositories;
using TravelBookingPortal.Api.Dtos.Staff;

namespace TravelBookingPortal.Api.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IPasswordHasher<Staff> _passwordHasher;

        public StaffService(IStaffRepository staffRepository, IPasswordHasher<Staff> passwordHasher)
        {
            _staffRepository = staffRepository;
            _passwordHasher = passwordHasher;
        }

        public Task<IEnumerable<Staff>> GetAllStaffAsync()
            => _staffRepository.GetAllStaffAsync();

        public Task<Staff?> GetStaffByIdAsync(int id)
            => _staffRepository.GetStaffByIdAsync(id);

        public async Task<Staff> CreateStaffAsync(CreateStaffDto createStaffDto)
        {
            var staff = new Staff
            {
                Name = createStaffDto.Name,
                Email = createStaffDto.Email,
                JobTitle = createStaffDto.JobTitle,
                BranchId = createStaffDto.BranchId
            };

            staff.PasswordHash = _passwordHasher.HashPassword(staff, createStaffDto.Password);

            return await _staffRepository.AddStaffAsync(staff);
        }

        public Task<Staff?> UpdateStaffAsync(int id, UpdateStaffDto updateStaffDto)
        {
            var staff = new Staff
            {
                Name = updateStaffDto.Name,
                Email = updateStaffDto.Email,
                JobTitle = updateStaffDto.JobTitle,
                BranchId = updateStaffDto.BranchId
            };

            return _staffRepository.UpdateStaffAsync(id, staff);
        }

        public async Task<Staff?> PatchStaffAsync(int id, StaffPatchDto patch)
        {
            var existing = await _staffRepository.GetStaffByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            if (patch.Name is not null)
            {
                existing.Name = patch.Name;
            }

            if (patch.Email is not null)
            {
                existing.Email = patch.Email;
            }

            if (patch.JobTitle is not null)
            {
                existing.JobTitle = patch.JobTitle;
            }

            if (patch.BranchId.HasValue)
            {
                existing.BranchId = patch.BranchId.Value;
            }

            if (patch.Password is not null)
            {
                existing.PasswordHash = _passwordHasher.HashPassword(existing, patch.Password);
            }

            return await _staffRepository.UpdateStaffAsync(id, existing);
        }

        public Task<bool> DeleteStaffAsync(int id)
            => _staffRepository.DeleteStaffAsync(id);
    }
}
