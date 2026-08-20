using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace TravelBookingPortal.Api.Repositories
{
    public class TourPackageRepository : ITourPackageRepository
    {
        private readonly AppDbContext _dbContext;

        public TourPackageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<TourPackage>> GetAllTourPackagesAsync()
        {
            return await _dbContext.TourPackages.Include(tp => tp.Destination).ToListAsync();
        }

        public async Task<TourPackage?> GetTourPackageByIdAsync(int id)
        {
            return await _dbContext.TourPackages.Include(tp => tp.Destination).FirstOrDefaultAsync(tp => tp.Id == id);
        }

        public async Task<TourPackage> AddTourPackageAsync(TourPackage tourPackage)
        {
            _dbContext.TourPackages.Add(tourPackage);
            await _dbContext.SaveChangesAsync();
            return tourPackage;
        }

        public async Task<TourPackage?> UpdateTourPackageAsync(int id, TourPackage tourPackage)
        {
            var existing = await _dbContext.TourPackages.FindAsync(id);
            if (existing is null)
            {
                return null;
            }

            existing.Name = tourPackage.Name;
            existing.DestinationId = tourPackage.DestinationId;
            existing.Price = tourPackage.Price;
            existing.DurationDays = tourPackage.DurationDays;
            existing.Description = tourPackage.Description;
            existing.SeatsAvailable = tourPackage.SeatsAvailable;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteTourPackageAsync(int id)
        {
            var tp = await _dbContext.TourPackages.FindAsync(id);
            if (tp is null)
            {
                return false;
            }

            _dbContext.TourPackages.Remove(tp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
