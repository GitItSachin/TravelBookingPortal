using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Customer;
public class CustomerPatchDto
{

    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [MaxLength(20)]
    public string? Phone { get; set; } = string.Empty;
}