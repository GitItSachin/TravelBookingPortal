using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Repositories;
using TravelBookingPortal.Api.Dtos.Destination;

namespace TravelBookingPortal.Api.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _destinationRepository;

        public DestinationService(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        public Task<IEnumerable<Destination>> GetAllDestinationsAsync()
            => _destinationRepository.GetAllDestinationsAsync();

        public Task<Destination?> GetDestinationByIdAsync(int id)
            => _destinationRepository.GetDestinationByIdAsync(id);

        public async Task<Destination> CreateDestinationAsync(CreateDestinationDto createDestinationDto)
        {
            var destination = new Destination
            {
                Name = createDestinationDto.Name,
                Country = createDestinationDto.Country
            };

            await _destinationRepository.AddDestinationAsync(destination);
            return destination;
        }

        public async Task<Destination?> UpdateDestinationAsync(int id, UpdateDestinationDto updateDestinationDto)
        {
            var destination = new Destination
            {
                Name = updateDestinationDto.Name,
                Country = updateDestinationDto.Country
            };
            return await _destinationRepository.UpdateDestinationAsync(id, destination);
        }

        public async Task<Destination?> PatchDestinationAsync(int id, DestinationPatchDto patch)
        {
            var existing = await _destinationRepository.GetDestinationByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            if (patch.Name != null)
            {
                existing.Name = patch.Name;
            }

            if (patch.Country != null)
            {
                existing.Country = patch.Country;
            }

            await _destinationRepository.UpdateDestinationAsync(id, existing);
            return existing;
        }

        public async Task<bool> DeleteDestinationAsync(int id)
        {
            var deleted = await _destinationRepository.DeleteDestinationAsync(id);
            return deleted != null;
        }
    }
}
