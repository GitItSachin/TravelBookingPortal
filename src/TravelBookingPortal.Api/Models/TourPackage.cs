using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingPortal.Api.Models
{
    public class TourPackage
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int DestinationId { get; set; }
        public Destination? Destination { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int DurationDays { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public int SeatsAvailable { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
