using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Destination;

public class CreateDestinationDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Country { get; set; }
}
