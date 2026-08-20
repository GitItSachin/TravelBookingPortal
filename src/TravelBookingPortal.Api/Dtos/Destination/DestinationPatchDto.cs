using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Destination;

public class DestinationPatchDto
{
    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }
}
