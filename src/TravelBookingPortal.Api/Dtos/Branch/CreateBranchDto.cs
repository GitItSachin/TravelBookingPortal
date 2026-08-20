using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Branch;

public class CreateBranchDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public required string City { get; set; }
}