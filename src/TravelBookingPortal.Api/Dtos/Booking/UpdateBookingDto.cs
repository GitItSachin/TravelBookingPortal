using System.ComponentModel.DataAnnotations;
using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Dtos.Booking;

public class UpdateBookingDto
{
    [Required]
    public required DateTime TravelDate { get; set; }

    [Required]
    [Range(1, 50)]
    public required int NumberOfTravelers { get; set; }

    [Required]
    public required BookingStatus Status { get; set; }
}
