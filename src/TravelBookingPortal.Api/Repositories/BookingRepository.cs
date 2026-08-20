using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace TravelBookingPortal.Api.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbContext;

        public BookingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _dbContext.Bookings
                .Include(b => b.Customer)
                .Include(b => b.TourPackage)
                .Include(b => b.Staff)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _dbContext.Bookings
                .Include(b => b.Customer)
                .Include(b => b.TourPackage)
                .Include(b => b.Staff)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> UpdateBookingAsync(int id, Booking booking)
        {
            var existing = await _dbContext.Bookings.FindAsync(id);
            if (existing is null)
            {
                return null;
            }

            existing.TravelDate = booking.TravelDate;
            existing.NumberOfTravelers = booking.NumberOfTravelers;
            existing.Status = booking.Status;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var existing = await _dbContext.Bookings.FindAsync(id);
            if (existing is null)
            {
                return false;
            }

            _dbContext.Bookings.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
