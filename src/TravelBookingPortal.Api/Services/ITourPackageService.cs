using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.TourPackage;

namespace TravelBookingPortal.Api.Services
{
    public interface ITourPackageService
    {
        Task<IEnumerable<TourPackage>> GetAllTourPackagesAsync();
        Task<TourPackage?> GetTourPackageByIdAsync(int id);
        Task<TourPackage> CreateTourPackageAsync(CreateTourPackageDto createTourPackageDto);
        Task<TourPackage?> UpdateTourPackageAsync(int id, UpdateTourPackageDto updateTourPackageDto);
        Task<bool> DeleteTourPackageAsync(int id);
        Task<TourPackage?> PatchTourPackageAsync(int id, TourPackagePatchDto patch);
    }
}
