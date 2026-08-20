using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Repositories
{
    public interface ITourPackageRepository
    {
        Task<TourPackage?> GetTourPackageByIdAsync(int id);
        Task<IEnumerable<TourPackage>> GetAllTourPackagesAsync();
        Task<TourPackage> AddTourPackageAsync(TourPackage tourPackage);
        Task<TourPackage?> UpdateTourPackageAsync(int id, TourPackage tourPackage);
        Task<bool> DeleteTourPackageAsync(int id);
    }
}
