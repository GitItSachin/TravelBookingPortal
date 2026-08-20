using Microsoft.EntityFrameworkCore;
using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Destination> Destinations => Set<Destination>();
        public DbSet<Staff> Staff => Set<Staff>();
        public DbSet<TourPackage> TourPackages => Set<TourPackage>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Booking> Bookings => Set<Booking>();

    }
}
