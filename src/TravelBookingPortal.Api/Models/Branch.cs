using System.ComponentModel.DataAnnotations;

namespace TravelBookingPortal.Api.Models;

public class Branch
{
public int Id { get; set; }
[MaxLength(200)]
public required string Name { get; set; }
[MaxLength(100)]
public required string City { get; set; }
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}