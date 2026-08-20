namespace TravelBookingPortal.Api.Dtos.Auth;

public class AuthResponseDto
{
    public required string Token { get; set; }
    public required DateTime ExpiresAtUtc { get; set; }
    public required string Role { get; set; }
}
