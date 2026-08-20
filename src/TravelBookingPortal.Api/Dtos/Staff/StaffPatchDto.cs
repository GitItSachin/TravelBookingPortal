using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Staff;

public class StaffPatchDto
{
    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }

    [MaxLength(100)]
    public string? JobTitle { get; set; }

    public int? BranchId { get; set; }

    [MinLength(8)]
    public string? Password { get; set; }
}
