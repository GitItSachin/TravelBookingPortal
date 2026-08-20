using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Repositories;
using TravelBookingPortal.Api.Dtos.Booking;
using TravelBookingPortal.Api.Exceptions;

namespace TravelBookingPortal.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITourPackageRepository _tourPackageRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IStaffRepository _staffRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            ITourPackageRepository tourPackageRepository,
            ICustomerRepository customerRepository,
            IStaffRepository staffRepository)
        {
            _bookingRepository = bookingRepository;
            _tourPackageRepository = tourPackageRepository;
            _customerRepository = customerRepository;
            _staffRepository = staffRepository;
        }

        public Task<IEnumerable<Booking>> GetAllBookingsAsync()
            => _bookingRepository.GetAllBookingsAsync();

        public Task<Booking?> GetBookingByIdAsync(int id)
            => _bookingRepository.GetBookingByIdAsync(id);

        public async Task<Booking> CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(createBookingDto.CustomerId);
            if (customer is null)
            {
                throw new NotFoundException($"Customer with ID {createBookingDto.CustomerId} not found.");
            }

            var staff = await _staffRepository.GetStaffByIdAsync(createBookingDto.StaffId);
            if (staff is null)
            {
                throw new NotFoundException($"Staff with ID {createBookingDto.StaffId} not found.");
            }

            var package = await _tourPackageRepository.GetTourPackageByIdAsync(createBookingDto.TourPackageId);
            if (package is null)
            {
                throw new NotFoundException($"TourPackage with ID {createBookingDto.TourPackageId} not found.");
            }

            if (package.SeatsAvailable < createBookingDto.NumberOfTravelers)
            {
                throw new InsufficientSeatsException(
                    $"Only {package.SeatsAvailable} seat(s) available for '{package.Name}', requested {createBookingDto.NumberOfTravelers}.");
            }

            package.SeatsAvailable -= createBookingDto.NumberOfTravelers;

            var booking = new Booking
            {
                CustomerId = createBookingDto.CustomerId,
                TourPackageId = createBookingDto.TourPackageId,
                StaffId = createBookingDto.StaffId,
                TravelDate = createBookingDto.TravelDate,
                NumberOfTravelers = createBookingDto.NumberOfTravelers,
                TotalAmount = package.Price * createBookingDto.NumberOfTravelers,
                Status = BookingStatus.Pending
            };

            var created = await _bookingRepository.AddBookingAsync(booking);
            return await _bookingRepository.GetBookingByIdAsync(created.Id)
                ?? created;
        }

        public Task<Booking?> UpdateBookingAsync(int id, UpdateBookingDto updateBookingDto)
        {
            var booking = new Booking
            {
                TravelDate = updateBookingDto.TravelDate,
                NumberOfTravelers = updateBookingDto.NumberOfTravelers,
                Status = updateBookingDto.Status
            };

            return _bookingRepository.UpdateBookingAsync(id, booking);
        }

        public async Task<Booking?> PatchBookingAsync(int id, BookingPatchDto patch)
        {
            var existing = await _bookingRepository.GetBookingByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            if (patch.TravelDate.HasValue)
            {
                existing.TravelDate = patch.TravelDate.Value;
            }

            if (patch.NumberOfTravelers.HasValue)
            {
                existing.NumberOfTravelers = patch.NumberOfTravelers.Value;
            }

            if (patch.Status.HasValue)
            {
                existing.Status = patch.Status.Value;
            }

            return await _bookingRepository.UpdateBookingAsync(id, existing);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking is null)
            {
                return false;
            }

            var package = await _tourPackageRepository.GetTourPackageByIdAsync(booking.TourPackageId);
            if (package is not null)
            {
                package.SeatsAvailable += booking.NumberOfTravelers;
            }

            return await _bookingRepository.DeleteBookingAsync(id);
        }
    }
}
