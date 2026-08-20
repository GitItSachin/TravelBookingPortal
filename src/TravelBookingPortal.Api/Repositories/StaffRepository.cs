using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace TravelBookingPortal.Api.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly AppDbContext _dbContext;

        public StaffRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Staff>> GetAllStaffAsync()
        {
            return await _dbContext.Staff.Include(s => s.Branch).ToListAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await _dbContext.Staff.Include(s => s.Branch).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Staff?> GetStaffByEmailAsync(string email)
        {
            return await _dbContext.Staff.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<Staff> AddStaffAsync(Staff staff)
        {
            _dbContext.Staff.Add(staff);
            await _dbContext.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff?> UpdateStaffAsync(int id, Staff staff)
        {
            var existing = await _dbContext.Staff.FindAsync(id);
            if (existing is null)
            {
                return null;
            }

            existing.Name = staff.Name;
            existing.Email = staff.Email;
            existing.JobTitle = staff.JobTitle;
            existing.BranchId = staff.BranchId;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            var staff = await _dbContext.Staff.FindAsync(id);
            if (staff is null)
            {
                return false;
            }

            _dbContext.Staff.Remove(staff);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
