using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Repositories
{
    public interface IDestinationRepository
    {
        Task<Destination?> GetDestinationByIdAsync(int id);
        Task<IEnumerable<Destination>> GetAllDestinationsAsync();
        Task AddDestinationAsync(Destination destination);
        Task<Destination?> UpdateDestinationAsync(int id, Destination destination);
        Task<Destination?> DeleteDestinationAsync(int id);
    }
}