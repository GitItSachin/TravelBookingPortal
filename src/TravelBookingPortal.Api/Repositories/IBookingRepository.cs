using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> AddBookingAsync(Booking booking);
        Task<Booking?> UpdateBookingAsync(int id, Booking booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}
