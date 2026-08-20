using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Staff;

public class CreateStaffDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public required string JobTitle { get; set; }

    [Required]
    public required int BranchId { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}
