using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingPortal.Api.Dtos.TourPackage;

public class CreateTourPackageDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [Required]
    public required int DestinationId { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public required decimal Price { get; set; }

    [Required]
    public required int DurationDays { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public required int SeatsAvailable { get; set; }
}
