using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.TourPackage;

public class TourPackagePatchDto
{
    [MaxLength(200)]
    public string? Name { get; set; }

    public int? DestinationId { get; set; }

    public decimal? Price { get; set; }

    public int? DurationDays { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public int? SeatsAvailable { get; set; }
}
