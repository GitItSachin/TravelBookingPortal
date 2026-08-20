using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.Destination;

namespace TravelBookingPortal.Api.Services
{
    public interface IDestinationService
    {
        Task<IEnumerable<Destination>> GetAllDestinationsAsync();
        Task<Destination?> GetDestinationByIdAsync(int id);
        Task<Destination> CreateDestinationAsync(CreateDestinationDto createDestinationDto);
        Task<Destination?> UpdateDestinationAsync(int id, UpdateDestinationDto updateDestinationDto);
        Task<bool> DeleteDestinationAsync(int id);
        Task<Destination?> PatchDestinationAsync(int id, DestinationPatchDto patch);
    }
}
