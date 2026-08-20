using TravelBookingPortal.Api.Dtos.Auth;

namespace TravelBookingPortal.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
