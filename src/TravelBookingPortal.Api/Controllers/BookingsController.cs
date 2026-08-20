using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Services;
using TravelBookingPortal.Api.Dtos.Booking;

namespace TravelBookingPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking is null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] CreateBookingDto createBookingDto)
        {
            var created = await _bookingService.CreateBookingAsync(createBookingDto);
            return CreatedAtAction(nameof(GetBookingById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Booking>> UpdateBooking(int id, [FromBody] UpdateBookingDto updateBookingDto)
        {
            var updated = await _bookingService.UpdateBookingAsync(id, updateBookingDto);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Booking>> PatchBooking(int id, [FromBody] BookingPatchDto patch)
        {
            var updated = await _bookingService.PatchBookingAsync(id, patch);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            var deleted = await _bookingService.DeleteBookingAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
