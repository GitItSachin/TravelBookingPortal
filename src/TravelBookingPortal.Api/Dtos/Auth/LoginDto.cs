using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Dtos.Auth;

public class LoginDto
{
    [Required]
    [MaxLength(200)]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
