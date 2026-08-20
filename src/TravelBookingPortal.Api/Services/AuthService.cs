using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPortal.Api.Dtos.Auth;
using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Repositories;

namespace TravelBookingPortal.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IPasswordHasher<Staff> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(
            IStaffRepository staffRepository,
            IPasswordHasher<Staff> passwordHasher,
            IConfiguration configuration)
        {
            _staffRepository = staffRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var staff = await _staffRepository.GetStaffByEmailAsync(loginDto.Email);
            if (staff is null)
            {
                return null;
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(staff, staff.PasswordHash, loginDto.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var expiresAtUtc = DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "60"));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, staff.Id.ToString()),
                new(ClaimTypes.NameIdentifier, staff.Id.ToString()),
                new(ClaimTypes.Email, staff.Email),
                new(ClaimTypes.Name, staff.Name),
                new(ClaimTypes.Role, staff.Role.ToString())
            };

            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("Jwt:Key is not configured.")));

            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiresAtUtc,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponseDto
            {
                Token = tokenString,
                ExpiresAtUtc = expiresAtUtc,
                Role = staff.Role.ToString()
            };
        }
    }
}
