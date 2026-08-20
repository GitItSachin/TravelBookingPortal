using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace TravelBookingPortal.Api.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly AppDbContext _dbContext;
        public DestinationRepository (AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IEnumerable<Destination>> GetAllDestinationsAsync()
        {
            return await _dbContext.Destinations.ToListAsync();
        }
        public async Task<Destination?> GetDestinationByIdAsync(int id)
        {
            return await _dbContext.Destinations.FindAsync(id);
        }

        public async Task AddDestinationAsync(Destination destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            _dbContext.Destinations.Add(destination);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Destination?> UpdateDestinationAsync(int id, Destination destination)
        {
            var existingDestination = await _dbContext.Destinations.FindAsync(id);
            if (existingDestination is null)
            {
                return null;
            }

            existingDestination.Name = destination.Name;
            existingDestination.Country = destination.Country;
            // Update other properties as needed

            await _dbContext.SaveChangesAsync();
            return existingDestination;
        }
        public async Task<Destination?> DeleteDestinationAsync(int id)
        {
            var destination = await _dbContext.Destinations.FindAsync(id);
            if (destination is null)
            {
                return null;
            }

            _dbContext.Destinations.Remove(destination);
            await _dbContext.SaveChangesAsync();
            return destination;
        }
    }

}