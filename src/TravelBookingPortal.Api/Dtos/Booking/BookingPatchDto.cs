using System.ComponentModel.DataAnnotations;
using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Dtos.Booking;

public class BookingPatchDto
{
    public DateTime? TravelDate { get; set; }

    [Range(1, 50)]
    public int? NumberOfTravelers { get; set; }

    public BookingStatus? Status { get; set; }
}
