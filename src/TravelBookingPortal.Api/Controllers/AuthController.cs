using Microsoft.AspNetCore.Mvc;
using TravelBookingPortal.Api.Dtos.Auth;
using TravelBookingPortal.Api.Services;

namespace TravelBookingPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result is null)
            {
                return Unauthorized(new { error = "Invalid email or password." });
            }

            return Ok(result);
        }
    }
}
