using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Repositories;
using TravelBookingPortal.Api.Dtos.TourPackage;

namespace TravelBookingPortal.Api.Services
{
    public class TourPackageService : ITourPackageService
    {
        private readonly ITourPackageRepository _tourPackageRepository;

        public TourPackageService(ITourPackageRepository tourPackageRepository)
        {
            _tourPackageRepository = tourPackageRepository;
        }

        public Task<IEnumerable<TourPackage>> GetAllTourPackagesAsync()
            => _tourPackageRepository.GetAllTourPackagesAsync();

        public Task<TourPackage?> GetTourPackageByIdAsync(int id)
            => _tourPackageRepository.GetTourPackageByIdAsync(id);

        public async Task<TourPackage> CreateTourPackageAsync(CreateTourPackageDto createTourPackageDto)
        {
            var tp = new TourPackage
            {
                Name = createTourPackageDto.Name,
                DestinationId = createTourPackageDto.DestinationId,
                Price = createTourPackageDto.Price,
                DurationDays = createTourPackageDto.DurationDays,
                Description = createTourPackageDto.Description,
                SeatsAvailable = createTourPackageDto.SeatsAvailable
            };

            return await _tourPackageRepository.AddTourPackageAsync(tp);
        }

        public Task<TourPackage?> UpdateTourPackageAsync(int id, UpdateTourPackageDto updateTourPackageDto)
        {
            var tp = new TourPackage
            {
                Name = updateTourPackageDto.Name,
                DestinationId = updateTourPackageDto.DestinationId,
                Price = updateTourPackageDto.Price,
                DurationDays = updateTourPackageDto.DurationDays,
                Description = updateTourPackageDto.Description,
                SeatsAvailable = updateTourPackageDto.SeatsAvailable
            };

            return _tourPackageRepository.UpdateTourPackageAsync(id, tp);
        }

        public async Task<TourPackage?> PatchTourPackageAsync(int id, TourPackagePatchDto patch)
        {
            var existing = await _tourPackageRepository.GetTourPackageByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            if (patch.Name is not null)
            {
                existing.Name = patch.Name;
            }

            if (patch.DestinationId.HasValue)
            {
                existing.DestinationId = patch.DestinationId.Value;
            }

            if (patch.Price.HasValue)
            {
                existing.Price = patch.Price.Value;
            }

            if (patch.DurationDays.HasValue)
            {
                existing.DurationDays = patch.DurationDays.Value;
            }

            if (patch.Description is not null)
            {
                existing.Description = patch.Description;
            }

            if (patch.SeatsAvailable.HasValue)
            {
                existing.SeatsAvailable = patch.SeatsAvailable.Value;
            }

            return await _tourPackageRepository.UpdateTourPackageAsync(id, existing);
        }

        public Task<bool> DeleteTourPackageAsync(int id)
            => _tourPackageRepository.DeleteTourPackageAsync(id);
    }
}
