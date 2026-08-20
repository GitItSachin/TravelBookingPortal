using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Branch;

public class BranchPatchDto
{
    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }
}