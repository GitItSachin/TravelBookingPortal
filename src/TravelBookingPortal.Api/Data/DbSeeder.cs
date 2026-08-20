using Bogus;
using Microsoft.EntityFrameworkCore;
using TravelBookingPortal.Api.Models;

namespace TravelBookingPortal.Api.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Branches.AnyAsync())
            {
                return;
            }

            var branchFaker = new Faker<Branch>()
                .RuleFor(b => b.Name, f => f.Company.CompanyName())
                .RuleFor(b => b.City, f => f.Address.City());

            var branches = branchFaker.Generate(20);
            context.Branches.AddRange(branches);
            await context.SaveChangesAsync();

            var destinationFaker = new Faker<Destination>()
                .RuleFor(d => d.Name, f => f.Address.City())
                .RuleFor(d => d.Country, f => f.Address.Country());

            var destinations = destinationFaker.Generate(20);
            context.Destinations.AddRange(destinations);
            await context.SaveChangesAsync();

            var staffFaker = new Faker<Staff>()
                .RuleFor(s => s.Name, f => f.Name.FullName())
                .RuleFor(s => s.Email, f => f.Internet.Email())
                .RuleFor(s => s.JobTitle, f => f.Name.JobTitle())
                .RuleFor(s => s.BranchId, f => f.PickRandom(branches).Id);

            var staff = staffFaker.Generate(20);
            context.Staff.AddRange(staff);
            await context.SaveChangesAsync();

            var packageFaker = new Faker<TourPackage>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.DestinationId, f => f.PickRandom(destinations).Id)
                .RuleFor(p => p.Price, f => f.Random.Decimal(100, 5000))
                .RuleFor(p => p.DurationDays, f => f.Random.Int(2, 14))
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.SeatsAvailable, f => f.Random.Int(5, 50));

            var packages = packageFaker.Generate(20);
            context.TourPackages.AddRange(packages);
            await context.SaveChangesAsync();
        }
    }
}