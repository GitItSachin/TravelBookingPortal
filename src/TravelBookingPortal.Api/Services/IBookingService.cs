using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Dtos.Booking;

namespace TravelBookingPortal.Api.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<Booking> CreateBookingAsync(CreateBookingDto createBookingDto);
        Task<Booking?> UpdateBookingAsync(int id, UpdateBookingDto updateBookingDto);
        Task<Booking?> PatchBookingAsync(int id, BookingPatchDto patch);
        Task<bool> DeleteBookingAsync(int id);
    }
}
