using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TravelBookingPortal.Api.Models;

public enum Role
{
    Admin,
    Manager,
    Staff
}
public class Staff
{
    public int Id { get; set; }
    [MaxLength(200)]
    public required string Name { get; set; }

    [MaxLength(200)]
    public required string Email { get; set; }
    [MaxLength(100)]
    public required string JobTitle { get; set; }  
    public int BranchId { get; set; }
    public Branch? Branch { get; set; }
    public Role Role { get; set; } = Role.Staff;
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}