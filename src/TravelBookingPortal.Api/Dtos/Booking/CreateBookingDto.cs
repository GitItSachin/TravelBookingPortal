using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Booking;

public class CreateBookingDto
{
    [Required]
    public required int CustomerId { get; set; }

    [Required]
    public required int TourPackageId { get; set; }

    [Required]
    public required int StaffId { get; set; }

    [Required]
    public required DateTime TravelDate { get; set; }

    [Required]
    [Range(1, 50)]
    public required int NumberOfTravelers { get; set; }
}
